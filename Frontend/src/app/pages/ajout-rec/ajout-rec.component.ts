import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-ajout-rec',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './ajout-rec.component.html',
  styleUrls: ['./ajout-rec.component.css']
})
export class AjoutRecComponent {
  reclamation = {
    paymentMethod: '',
    service: '',
    receptionChannel: '',
    email: '',
    tel: '',
    description: ''
  };

  constructor(private http: HttpClient) {}

  onSubmit() {
    // Vérifier que tous les champs sont remplis
    if (this.reclamation.paymentMethod === '' || 
        this.reclamation.service === '' || 
        this.reclamation.receptionChannel === '' || 
        this.reclamation.email === '' || 
        this.reclamation.tel === '' || 
        this.reclamation.description === '') {
      alert('Complétez la saisie du formulaire');
    } else {
      const clientId = localStorage.getItem('clientId'); 
      const reclamationData = {
        servs: this.reclamation.service,
        canalReception: this.reclamation.receptionChannel,
        descpt: this.reclamation.description,
        mail: this.reclamation.email,
        TelRec: this.reclamation.tel,
        etat: 'En cours',
        idClient: clientId
      };

      // Définir les en-têtes pour la requête HTTP
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });

      // Envoyer la requête POST
      this.http.post('http://localhost:5264/api/AjoutRec/ajoutreclamation', 
                     reclamationData, 
                     { headers: headers })
        .subscribe(
          (res: any) => {
            if (res.message === "OK") {
              alert("Réclamation ajoutée avec succès");
            } else {
              alert("Erreur lors de l'ajout de la réclamation");
            }
          },
          error => {
            console.error('Erreur lors de l\'appel API:', error);
            alert("Erreur lors de l'appel à l'API : " + error.message);
          }
        );
    }
  }
}
