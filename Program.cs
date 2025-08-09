using System;
using MySqlConnector;

namespace HitungKalori
{
    class Program
    {
        static string connectionString = "Server=localhost;Database=kebutuhankalori;User ID=root;Password=;";
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----SELAMAT DATANG DI APLIKASI HITUNG KALORI -----");
                Console.WriteLine("1. Registrasi");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Keluar");
                Console.WriteLine("Pilih opsi di atas:");
                string? pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        Registrasi();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opsi yang anda masukkan tidak ada");
                        break;
                }
            }
        }

        static void Registrasi()
        {
            Console.Clear();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("----- HALAMAN REGISTRASI -----");
                Console.Write("Masukkan username: ");
                string? username = Console.ReadLine();

                Console.Write("Masukkan email: ");
                string? email = Console.ReadLine();

                Console.Write("Masukkan password: ");
                string? password = Console.ReadLine();

                string? ulangipassword;

                while (true)
                {
                    Console.Write("Ulangi password: ");
                    ulangipassword = Console.ReadLine();

                    if (ulangipassword != password)
                    {
                        Console.WriteLine("Password tidak sama, silahkan masukkan ulang");
                        Console.ReadKey();
                    }
                    else
                    {
                        string query = "INSERT INTO users (username, password, email) VALUES (@username, @password, @email)";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@password", password);
                            cmd.Parameters.AddWithValue("@email", email);

                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Registrasi berhasil!");

                            Console.WriteLine("Tekan enter untuk melanjutkan");
                            Console.ReadLine();
                            break;
                        
                        }
                    }
                } 
            }
        }
        static void Login()
        {
            Console.Clear();
            while (true)
            {
            
            int userId = -1;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    
                Console.WriteLine("------ HALAMAN LOGIN -----");
                Console.Write("Username : ");
                string? username = Console.ReadLine();

                Console.Write("Password : ");
                string? password = Console.ReadLine();

                
                    connection.Open();
                    string query = "SELECT id FROM users WHERE username = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, connection); 
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); 

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            
                            if (reader.Read())
                            {
                                userId = reader.GetInt32("id");
                                Console.WriteLine("Selamat datang " + username);
                                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                                Console.ReadLine();

                                 PilihMenu(userId);
                                 break;
                            }
                            else
                            {
                                Console.WriteLine("Username atau password salah.");
                                Console.WriteLine("Tekan Enter untuk kembali ke halaman login...");
                                Console.ReadLine();

                            }    
                        }
                        }
                
                }
    }                
        static void PilihMenu(int userId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----- HALAMAN MENU -----");
                Console.WriteLine("1. Hitung kebutuhan kalori");
                Console.WriteLine("2. Lihat data kalori");
                Console.WriteLine("3. Log out");
                Console.Write("Pilih opsi di atas: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        HitungKalori(userId);
                        break;
                    case "2":
                        LihatData(userId);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opsi yang anda masukkan tidak ada");
                        break;
                }
            } 
        }
        static void HitungKalori(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            while (true)
            {
                Console.Clear();
                connection.Open();
                Console.WriteLine("----- HALAMAN HITUNG KALORI -----");
                Console.Write("Masukkan Umur Anda (tahun): ");
                int? umur = Convert.ToInt32(Console.ReadLine());

                Console.Write("Masukkan Berat Badan Anda (kg): ");
                int? berat_badan = Convert.ToInt32(Console.ReadLine());

                Console.Write("Masukkan Tinggi Badan Anda (cm): ");
                int? tinggi_badan = Convert.ToInt32(Console.ReadLine());

                Console.Write("Masukkan Jenis Kelamin anda (L/P): ");
                string? jenis_kelamin = Console.ReadLine();

                double? kalori = -0;
                if (umur <= 0 || berat_badan <= 0 || tinggi_badan <= 0)
                {
                    Console.WriteLine("Input tidak valid. Pastikan umur, berat, dan tinggi badan berupa angka positif.");
                    Console.WriteLine("Tekan enter untuk melanjutkan...");
                    Console.ReadLine();
                    continue;   
                }
                if (jenis_kelamin != "L" && jenis_kelamin != "P")
                {
                    Console.WriteLine("Jenis kelamin tidak valid. Masukkan 'L' untuk laki-laki atau 'P' untuk perempuan.");
                    Console.WriteLine("Tekan enter untuk melanjutkan...");
                    Console.ReadLine();
                    continue;
                }
                if (jenis_kelamin == "L")
                {
                    kalori = (double)(66 + (13.7 * berat_badan) + (5 * tinggi_badan) - (6.8 * umur));
                    // Pria: BMR = 66 + (13,7 x berat badan dalam kg) + (5 x tinggi dalam cm) – (6,8 x umur dalam tahun)
                }
                else if (jenis_kelamin == "P")
                {
                    kalori = (double)(655 + (9.6 * berat_badan) + (1.8 * tinggi_badan) - (4.7 * umur));
                    // Wanita: BMR = 655 + (9.6 x berat badan dalam kg) + (1.8 x tinggi dalam cm) – (4.7 x umur dalam tahun)
                }
            Console.WriteLine("Kebutuhan kalori harian anda adalah: " + kalori);
            Console.ReadLine();
            Console.WriteLine("Tingkat aktivitas anda");
            Console.WriteLine("1. Kurang aktif");
            Console.WriteLine("2. Aktif ringan");
            Console.WriteLine("3. Cuktp aktif");
            Console.WriteLine("4. Sangat aktif");
            Console.WriteLine("5. Exstra aktif");
            
            Console.WriteLine("Silahkan pilih opsi di atas (angka)");

            string? keaktivan = Console.ReadLine();
            double? faktorAktivitas, kalori_harian;

            switch (keaktivan)
            {
                case "1":
                    faktorAktivitas = 1.2;
                    break;
                case "2":
                    faktorAktivitas = 1.375;
                    break;
                case "3":
                    faktorAktivitas = 1.55;
                    break;
                case "4":
                    faktorAktivitas = 1.725;
                    break;
                case "5":
                    faktorAktivitas = 1.9;
                    break;
                default:
                    Console.WriteLine("Opsi yang anda masukkan tidak valid");
                    Console.WriteLine("Tekan enter untuk melanjutkan...");
                    Console.ReadLine();
                    continue;
            }
            kalori_harian = kalori * faktorAktivitas;
            Console.WriteLine("kebutuhan kalori harian anda dengan tingkat aktivitas yang anda pilih adalah " + kalori_harian + " kcl");
            
            string query = "INSERT INTO kalori_data (user_id, umur, berat_badan, tinggi_badan, jenis_kelamin, kalori, kalori_harian) " +
               "VALUES (@user_id, @umur, @berat_badan, @tinggi_badan, @jenis_kelamin, @kalori, @kalori_harian)";

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@user_id", userId);
cmd.Parameters.AddWithValue("@umur", umur);
cmd.Parameters.AddWithValue("@berat_badan", berat_badan);
cmd.Parameters.AddWithValue("@tinggi_badan", tinggi_badan);
cmd.Parameters.AddWithValue("@jenis_kelamin", jenis_kelamin);
cmd.Parameters.AddWithValue("@kalori", kalori);
cmd.Parameters.AddWithValue("@kalori_harian", kalori_harian);


                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Data berhasil ditambahkan!");

                            Console.WriteLine("Tekan enter untuk melanjutkan..");
                            Console.ReadLine();
                            break;
                        }
            }
        }
        static void LihatData(int userId)
        {
 using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        try
        {
            connection.Open();
            Console.Clear();
            Console.WriteLine("----- HALAMAN LIHAT DATA -----");
            Console.WriteLine("1. Tampilkan Semua Data Kalori");
            Console.WriteLine("2. Cari Data Berdasarkan ID User");
            Console.WriteLine("3. Kembali ke Menu Utama");
            Console.Write("Pilih opsi: ");
            string? opsi = Console.ReadLine();

            string query = "";

            switch (opsi)
            {
                case "1":
                    query = "SELECT * FROM kalori_data";
                    break;

                case "2":
                    Console.Write("Masukkan ID User yang ingin dicari: ");
                    string? idInput = Console.ReadLine();
                    query = "SELECT * FROM kalori_data WHERE user_id = @user_id";
                    break;

                case "3":
                    return; // Kembali ke menu utama
                default:
                    Console.WriteLine("Opsi tidak valid. Tekan Enter untuk kembali...");
                    Console.ReadLine();
                    return;
            }

            MySqlCommand cmd = new MySqlCommand(query, connection);
            if (opsi == "2")
            {
                Console.Write("Masukkan ID User yang ingin dicari: ");
                string? idInput = Console.ReadLine();
                cmd.Parameters.AddWithValue("@user_id", idInput);
            }

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("-----------------------------------------------------------------------------");
                Console.WriteLine("| ID | User ID | Umur | Berat Badan | Tinggi Badan | Kalori | kalori harian |");
                Console.WriteLine("-----------------------------------------------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine($"| {reader["id"],-3} | {reader["user_id"],-8} | {reader["umur"],-4} | {reader["berat_badan"],-11} | {reader["tinggi_badan"],-13} | {reader["kalori"],-6} | {reader["kalori"],-6} |");
                }
                Console.WriteLine("----------------------------------------------------------------------------");
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Terjadi kesalahan: " + ex.Message);
            Console.WriteLine("Tekan Enter untuk kembali...");  
            Console.ReadLine();
        }
    }
        }
    }
    }
