import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgbModal, NgbModule, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-liste-rec',
  standalone: true,
  imports: [CommonModule, NgbModule,FormsModule],
  templateUrl: './liste-rec.component.html',
  styleUrls: ['./liste-rec.component.css']
})
export class ListeRecComponent implements OnInit {
  reclamations: any[] = [];
  selectedReclamation: any;
  private modalRef: NgbModalRef | undefined;

  @ViewChild('content') contentModal: TemplateRef<any> | undefined;
  @ViewChild('confirmationModal') confirmationModal: TemplateRef<any> | undefined;
  @ViewChild('editModal') editModal: TemplateRef<any> | undefined;

  constructor(private http: HttpClient, private modalService: NgbModal) {}

  ngOnInit() {
    this.loadReclamations();
  }

  loadReclamations() {
    const clientId = localStorage.getItem('clientId');
    if (clientId) {
      this.http.get<any[]>(`http://localhost:5264/api/ListReclamation/${clientId}`)
        .subscribe(data => {
          this.reclamations = data;
        }, error => {
          console.error('Erreur lors de la récupération des réclamations', error);
        });
    } else {
      console.error('Client ID non trouvé dans le stockage local');
    }
  }

  open(content: TemplateRef<any>, reclamation: any) {
    this.selectedReclamation = reclamation;
    if (content) {
      this.modalRef = this.modalService.open(content);
    }
  }

  openConfirmationModal() {
    if (this.confirmationModal) {
      this.modalRef = this.modalService.open(this.confirmationModal);
    }
  }

  confirmDelete(modal: any) {
    if (this.selectedReclamation) {
      const clientId = localStorage.getItem('clientId');
      if (clientId) {
        this.http.delete<boolean>(`http://localhost:5264/api/SupprimerRec/${clientId}/${this.selectedReclamation.idRec}`)
          .subscribe(result => {
            if (result) {
              this.reclamations = this.reclamations.filter(r => r.idRec !== this.selectedReclamation.idRec);
              modal.close();
              alert('Réclamation supprimée avec succès');
              this.selectedReclamation = null;
            } else {
              console.error('La réclamation n\'a pas pu être supprimée');
            }
          }, error => {
            console.error('Erreur lors de la suppression de la réclamation', error);
          });
      } else {
        console.error('Client ID non trouvé dans le stockage local');
      }
    }
  }

  openEditModal() {
    if (this.editModal) {
      this.modalRef = this.modalService.open(this.editModal);
    }
  }
  submitEdit() {
    if (this.selectedReclamation) {
      const clientId = localStorage.getItem('clientId');
      if (clientId) {
        this.http.put<boolean>(`http://localhost:5264/api/ModifierRec/modifierreclamation/${clientId}/${this.selectedReclamation.idRec}`, this.selectedReclamation)
          .subscribe(result => {
            if (result) {
              this.loadReclamations(); // Recharge la liste après modification
              this.modalRef?.close();
              alert('Réclamation modifiée avec succès');
            } else {
              console.error('La réclamation n\'a pas pu être modifiée');
            }
          }, error => {
            console.error('Erreur lors de la modification de la réclamation', error);
          });
      } else {
        console.error('Client ID non trouvé dans le stockage local');
      }
    }
  }
}
