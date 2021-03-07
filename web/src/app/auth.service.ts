import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  url = environment.apiUrl + "login/authenticate";
  constructor(private http: HttpClient) {}

  authLogin(user) {
    return this.http.post(this.url, user, {
      responseType: "text",
    });
  }

  getUser() {
    return this.http.get(environment.apiUrl + "login");
  }

  getApproval() {
    return this.http.get(environment.apiUrl + "approval");
  }

  createApproval(user: {
    leaveDate: string;
    userId: number;
    comment?: string;
    status?: number;
  }) {
    return this.http.post(environment.apiUrl + "approval", user);
  }

  EditApproval(
    id: number,
    user: {
      leaveDate: string;
      userId: number;
      comment?: string;
      status: number;
    }
  ) {
    return this.http.put(environment.apiUrl + "approval/" + id, user);
  }

  DeleteApproval(id: number) {
    return this.http.delete(environment.apiUrl + "approval/" + id);
  }
}
