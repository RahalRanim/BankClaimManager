import { Component,OnInit  } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  reclamationsCount: number = 0;
  pendingCount: number = 0;
  resolvedCount: number = 0;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    const clientId = localStorage.getItem('clientId');
    if (clientId) {
      this.http.get<any[]>(`http://localhost:5264/api/ListReclamation/${clientId}`)
        .subscribe(data => {
          this.reclamationsCount = data.length;
          this.pendingCount = data.filter(rec => rec.etat === 'En cours').length;
          this.resolvedCount = data.filter(rec => rec.etat === 'Traité').length;
        }, error => {
          console.error('Erreur lors de la récupération des réclamations', error);
        });
    } else {
      console.error('Client ID non trouvé dans le stockage local');
    }
  }
}