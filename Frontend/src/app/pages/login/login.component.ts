import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router,RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginObj: Login;

  constructor(private http:HttpClient,private router: Router){
    this.loginObj=new Login();
  }
  onLogin(){
    this.http.post('http://localhost:5264/api/Auth/authenticate', this.loginObj).subscribe((res:any)=>{
      if(res.status=="OK"){
        localStorage.setItem('clientId', res.idClient);
        this.router.navigate(['/home']);
        localStorage.setItem("login",this.loginObj.login);
        localStorage.setItem("type",res.type);
      }
      else{
        alert("Login ou mot de passe incorrect !");
      }
    })
  }

}



export class Login {
  login: string;
  mtp: string;

  constructor(){
    this.login='';
    this.mtp='';
  }
}