export interface User {
  id: string;
  fullName: string;
  username: string;
  email: string;
  roles: string[];   // ✅ burayı ekle
  // roles?: string[];  // şimdilik yoruma al
}