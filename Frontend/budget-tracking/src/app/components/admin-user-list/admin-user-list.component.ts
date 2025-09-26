import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-admin-user-list',
  templateUrl: './admin-user-list.component.html',
  styleUrls: ['./admin-user-list.component.css'] // stil dosyası da eklenebilir
})
export class AdminUserListComponent implements OnInit {
  users: User[] = [];
  loading: boolean = false;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  ngAfterViewInit(): void {
    const wrapper = document.getElementById("wrapper");
    const toggleButton = document.getElementById("sidebarToggle");

    if (wrapper && toggleButton) {
      toggleButton.addEventListener("click", () => {
        wrapper.classList.toggle("toggled");
      });
    }
  }

  // Kullanıcıları yükle
  loadUsers(): void {
    this.loading = true;
    this.userService.getUsers().subscribe({
      next: (data: User[]) => {
        this.users = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('❌ Kullanıcılar yüklenemedi', err);
        this.loading = false;
      }
    });
  }

  // Kullanıcı düzenleme
  editUser(user: User): void {
    console.log("✏️ Düzenlenecek kullanıcı:", user);
    // İleride modal açabilir veya ayrı bir sayfaya yönlendirebilirsin
  }

  // Kullanıcı silme
  deleteUser(id: string): void {
    if (confirm('⚠️ Bu kullanıcıyı silmek istediğine emin misin?')) {
      this.userService.deleteUser(id).subscribe({
        next: () => {
          console.log("✅ Kullanıcı silindi");
          this.loadUsers(); // listeyi güncelle
        },
        error: (err) => console.error('❌ Silme işlemi başarısız', err)
      });

      
    }
    
  }
}
