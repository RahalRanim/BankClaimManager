import { Routes } from '@angular/router';
import { importProvidersFrom } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { provideRouter } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ListeRecComponent } from './pages/liste-rec/liste-rec.component';
import { AjoutRecComponent } from './pages/ajout-rec/ajout-rec.component';
import { HomeComponent } from './pages/home/home.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'listeRec', component: ListeRecComponent },
      { path: 'ajoutRec', component: AjoutRecComponent }
    ]
  },
  { 
    path: 'dashboard', 
    component: DashboardComponent,
    children: [
      { path: 'listeRec', component: ListeRecComponent },
      { path: 'ajoutRec', component: AjoutRecComponent }
    ]
  },
  { path: 'listeRec', component: ListeRecComponent },
  { path: 'ajoutRec', component: AjoutRecComponent }
];

export const appProviders = [
  provideHttpClient(),
  provideRouter(routes),
  importProvidersFrom(BrowserModule, FormsModule)
];
