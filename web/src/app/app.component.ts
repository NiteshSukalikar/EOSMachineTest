import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "./auth.service";
import * as moment from "moment";
import { EventData } from "ngx-event-calendar/lib/interface/event-data";

export const testData: EventData[] = [];

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

  dataArray: EventData[] = testData;

  selectDay(date) {
    this.selectedDate = date;
    let currentdate = new Date();
    if (
      moment(this.selectedDate).isAfter(currentdate) ||
      moment(this.selectedDate).isSame(moment(), "day")
    ) {
      this.openModal();
    } else {
      alert("You cannot apply for leave");
    }
  }

  ngOnInit(): void {
    this.backdrop = false;
    this.initForm();
    this.approvalInitForm();
    this.getApproval();
    this.isLogin = true;
    this.isAdmin = false;
    this.isError = false;
  }

  getApproval() {
    this.authService.getApproval().subscribe((res) => {
      if (res) {
        this.list = res;
        this.isComment = true;
      }
    });
  }
  openModal(item?) {
    debugger;
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
    debugger;
    var currentDate =
      this.selectedDate.year +
      "-" +
      this.selectedDate.month +
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

  onApprovalSubmit(data, item?) {
    debugger;
    if (data === "Approved") {
      var Status = 2;
    } else {
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

    if (data === "Approved") {
     this.delete(item.id);
    } else {
      this.authService.EditApproval(this.rejectedData.id, model).subscribe((res) => {
        alert("Leave Rejected");
        this.delete(this.rejectedData.id);
      });
    }
    this.onCloseHandled();
  }

  delete(id){
    this.authService.DeleteApproval(id).subscribe((res) => {
      this.getApproval();
    });
  }
}
