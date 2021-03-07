import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  url = "http://localhost:51536/api/login/authenticate";
  constructor(private http: HttpClient) {}

  authLogin(user) {
    return this.http.post(this.url, user, {
      responseType: "text",
    });
  }

  getUser() {
    return this.http.get("http://localhost:51536/api/login");
  }

  getApproval() {
    return this.http.get("http://localhost:51536/api/approval");
  }

  createApproval(user : {
    leaveDate: string;
    userId: number;
    comment?: string;
    status?: number;
  }) {
    return this.http.post("http://localhost:51536/api/approval",user);
  }

  EditApproval(id:number, user : {
    leaveDate: string;
    userId: number;
    comment?: string;
    status: number;
  }) {
    return this.http.put("http://localhost:51536/api/approval/" + id ,user);
  }


  DeleteApproval(id:number)
  {
    return this.http.delete("http://localhost:51536/api/approval/" + id);
  }
}
