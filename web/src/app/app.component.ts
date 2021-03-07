import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "./auth.service";
import * as moment from "moment";
import { EventData } from "ngx-event-calendar/lib/interface/event-data";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent {
  loginForm: FormGroup;
  ApprovalForm: FormGroup;

  calendarOptions = { fromDate: moment() };
  calendarValue = null;

  loginData: any;

  Role = "";
  display = "none";
  Name: any;
  error: string;

  backdrop: boolean = false;
  isLogin = true;
  isAdmin = false;
  isError = false;
  isComment: boolean = false;
  list: Object;
  user: Object;
  selectedDate: any;
  rejectedData: any;

  constructor(private authService: AuthService) {}

  dataArray: EventData[] = [];

  selectDay(date) {
    this.selectedDate = date;
    let currentdate = new Date();
    if (
      moment(this.selectedDate).isAfter(currentdate) ||
      moment(this.selectedDate).isSame(moment(), "day")
    ) {
      let isAny = this.dataArray.find((c: any) => {
        if (
          moment(c.leaveDate).format("YYYY-MM-DD") ===
          moment(date).format("YYYY-MM-DD")
        ) {
          return c;
        }
      });
      if (isAny) {
        alert("You have already applied for a leave");
      } else {
        this.openModal();
      }
    } else {
      alert("You cannot apply for past date leave");
    }
  }

  ngOnInit(): void {
    this.backdrop = false;
    this.initForm();
    this.approvalInitForm();
    this.getApproval();
    if (localStorage.getItem("user")) {
      var user: any = JSON.parse(localStorage.getItem("user"));
      if (user.data.roleId === 1) {
        this.isAdmin = true;
      } else {
        this.isAdmin = false;
      }
      this.isLogin = false;
    } else {
      this.isLogin = true;
    }
    this.isError = false;
  }

  logout() {
    this.isLogin = true;
  }

  getApproval() {
    this.authService.getApproval().subscribe((res: any) => {
      if (res) {
        this.dataArray = res.map((c: any) => {
          if (c.status === 1) {
            c.desc = "Pending";
            c.color = "orange";
          } else if (c.status === 2) {
            c.desc = "Approved";
            c.color = "green";
          } else {
            c.desc = "Rejected";
            c.color = "red";
          }
          (c.startDate = moment(c.startDate).add(2, "hours")),
            (c.endDate = moment(c.endDate).add(2, "hours"));
          return c;
        });
        this.list = res;
        this.isComment = true;
      }
    });
  }
  openModal(item?) {
    this.display = "block";
    this.backdrop = true;
    if (item) {
      this.rejectedData = item;
    }
  }
  onCloseHandled() {
    this.display = "none";
    this.backdrop = false;
  }

  applyLeave() {
    var currentDate =
      this.selectedDate.year +
      "-" +
      (this.selectedDate.month + 1) +
      "-" +
      this.selectedDate.day;

    currentDate = moment(currentDate).format("YYYY-MM-DD");

    var userData = JSON.parse(localStorage.getItem("user"));

    const model = {
      leaveDate: currentDate,
      userId: userData.data.loginID,
      comment: "",
      status: 1,
    };

    if (userData) {
      this.authService.createApproval(model).subscribe((res) => {
        console.log(res);
      });
    }
    this.onCloseHandled();
  }

  initForm(): void {
    this.loginForm = new FormGroup({
      UserName: new FormControl(null, Validators.required),
      Password: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
      ]),
    });

    this.ApprovalForm = new FormGroup({
      Comment: new FormControl(null, Validators.required),
    });
  }

  approvalInitForm(): void {
    this.ApprovalForm = new FormGroup({
      Comment: new FormControl(null, Validators.required),
    });
  }

  onSubmit() {
    const user = this.loginForm.value;
    this.authService.authLogin(user).subscribe(
      (res: any) => {
        this.loginData = JSON.parse(res);
        localStorage.setItem("user", res);
        if (this.loginData.data.roleId == 1) {
          this.Role = "Admin";
          this.Name = this.loginData.data.name;
          this.isAdmin = true;
        } else {
          this.Role = "User";
          this.Name = this.loginData.data.name;
          this.isAdmin = false;
        }
        this.isLogin = false;
        this.isError = false;
      },
      (error) => {
        this.isError = true;
        this.error = "Invalid User Or Connection Problem";
      }
    );
  }

  onApprovalSubmit(data: any, item?: any) {
    if (data === "Approved") {
      var id = item.id;
      var Status = 2;
    } else {
      var id = this.rejectedData.id;
      var Status = 3;
    }

    if (item) {
      var model = {
        leaveDate: item.leaveDate,
        userId: item.userId,
        Comment: this.ApprovalForm.value.Comment,
        status: Status,
      };
    } else {
      var model = {
        leaveDate: this.rejectedData.leaveDate,
        userId: this.rejectedData.userId,
        Comment: this.ApprovalForm.value.Comment,
        status: Status,
      };
    }
    this.authService.EditApproval(id, model).subscribe((res) => {
      this.getApproval();
    });
    this.onCloseHandled();
  }

  delete(id: any) {
    this.authService.DeleteApproval(id).subscribe((res) => {
      this.getApproval();
    });
  }
}
