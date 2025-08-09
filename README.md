# Hitung Kalori App

Aplikasi console berbasis C# untuk menghitung kebutuhan kalori harian pengguna berdasarkan umur, berat badan, tinggi badan, jenis kelamin, dan tingkat aktivitas.

Fitur Utama
- Registrasi & Login menggunakan MySQL
- Menghitung kebutuhan kalori harian (BMR)
- Menyimpan data perhitungan ke database
- Melihat semua data kalori atau berdasarkan ID user

Teknologi yang Digunakan
- Bahasa: C#
- Database: MySQL
- Library: [MySqlConnector](https://mysqlconnector.net/)
- Platform: .NET 8.0

Cara Menjalankan
1. Pastikan sudah menginstal:
   - [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
   - [MySQL Server](https://dev.mysql.com/downloads/mysql/)
   - [Visual Studio Code](https://code.visualstudio.com/) (opsional)

2. Clone repository:
   ```bash
   git clone https://github.com/maszgusz/hitung-kalori.git
   cd hitung-kalori

3. Import Database
   - Buka MySQL Command Line atau aplikasi seperti phpMyAdmin
   - Buat database baru:
     CREATE DATABASE kebutuhankalori;
5. Konfigurasi Koneksi Database
   - Buka file Program.cs
   - Ubah Konfigurasi koneksi sesuai MySql Anda:
     string connectionString = "Server=localhost;Database=kebutuhankalori;User ID=root;Password=;";
7. Jalankan Aplikasi
   - dotnet run

