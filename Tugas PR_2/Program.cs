/*Robot:
Memiliki properti:
• nama(String): Nama robot.
• energi(int): Energi yang dimiliki robot.
• armor (int): Kekuatan pertahanan robot.
• serangan (int): Kekuatan serangan robot.
Memiliki metode:
• serang(robot target): Menyerang robot lain, mengurangi energinya berdasarkan 
kekuatan serangan.
• gunakanKemampuan(Kemampuan kemampuan): Menggunakan kemampuan khusus 
dari robot.
• cetakInformasi(): Menampilkan informasi tentang robot (nama, energi, armor,
serangan).

Bos Robot:
Memiliki properti:
• nama(String): Nama bos.
• energi(int): Energi yang dimiliki bos robot.
• pertahanan (int): Kekuatan pertahanan bos robot yang lebih besar dari robot biasa.
Memiliki metode:
• diserang(robot penyerang): Menerima serangan dari robot biasa, mengurangi energi.
• mati(): Metode yang akan dipanggil jika energi bos habis.

3. Kemampuan:
Buat beberapa kemampuan dengan efek yang berbeda, seperti:
• Perbaikan(Repair): Memulihkan energi robot.
• Serangan Listrik (Electric Shock): Menyerang musuh dengan efek listrik yang dapat 
mempengaruhi gerakan mereka.
• Serangan Plasma (Plasma Cannon): Menyerang dengan tembakan plasma yang 
menembus armor lawan.
• Pertahanan Super (Super Shield): Meningkatkan kekuatan armor sementara

Persyaratan Tambahan:
a.Gunakan abstract class untuk mendefinisikan fungsionalitas umum yang diwarisi oleh 
kelas lain (misalnya, kelas Robot).
b. Gunakan interface untuk mendefinisikan kontrak yang harus diimplementasikan oleh 
kelas lain (misalnya, interface Kemampuan untuk berbagai jenis skill).
c. Tambahkan beberapa jenis robot dengan kemampuan khusus mereka sendiri,
menggunakan polimorfisme.
d. Implementasikan inheritance dengan tepat, misalnya Bos Robot bisa menjadi turunan 
dari kelas Robot namun dengan kekuatan yang lebih besar.
e. Sertakan beberapa aturan permainan seperti energi robot yang harus dipulihkan di akhir 
setiap giliran.*/
using System;

class Program
{
    static void Main(string[] args)
    {
        // Membuat robot dan bos
        Robot robot1 = new RobotBiasa("Robot A", 100, 10, 20);
        Robot robot2 = new RobotBiasa("Robot B", 80, 15, 25);
        BosRobot bos = new BosRobot("Boss X", 200, 30, 35);

        // Membuat kemampuan
        IKemampuan repair = new Repair();
        IKemampuan electricShock = new ElectricShock();
        IKemampuan plasmaCannon = new PlasmaCannon();
        IKemampuan superShield = new SuperShield();

        // Menampilkan informasi robot
        robot1.CetakInformasi();
        robot2.CetakInformasi();
        bos.CetakInformasi();

        // Pertarungan dimulai
        robot1.Serang(bos);  // Robot A menyerang Bos
        bos.Diserang(robot1); // Bos diserang

        robot2.Serang(bos);  // Robot B menyerang Bos
        bos.Diserang(robot2); 

        // Gunakan kemampuan
        robot1.GunakanKemampuan(repair, robot1); // Robot A menggunakan perbaikan
        robot2.GunakanKemampuan(electricShock, bos); // Robot B menggunakan serangan listrik pada Bos
        robot2.GunakanKemampuan(plasmaCannon, bos); // Robot B menggunakan serangan plasma pada Bos
        robot1.GunakanKemampuan(superShield, robot1); // Robot A menggunakan pertahanan super

        // Cetak informasi setelah pertarungan
        robot1.CetakInformasi();
        robot2.CetakInformasi();
        bos.CetakInformasi();
    }
}

// Abstract Class Robot
abstract class Robot
{
    public string nama { get; set; }
    public int energi { get; set; }
    public int armor { get; set; }
    public int serangan { get; set; }

    public Robot(string nama, int energi, int armor, int serangan)
    {
        this.nama = nama;
        this.energi = energi;
        this.armor = armor;
        this.serangan = serangan;
    }

    // Method serang
    public void Serang(Robot target)
    {
        int damage = this.serangan - target.armor;  // Mengurangi damage dengan armor target
        if (damage < 0)
        {
            damage = 0;  // Jika damage negatif, set menjadi 0
        }
        target.energi -= damage;
        Console.WriteLine($"{this.nama} menyerang {target.nama}, menyebabkan {damage} damage!");
    }

    // Abstract method untuk menggunakan kemampuan
    public abstract void GunakanKemampuan(IKemampuan kemampuan, Robot target);

    // Menampilkan informasi robot
    public void CetakInformasi()
    {
        Console.WriteLine($"Nama: {nama}, Energi: {energi}, Armor: {armor}, Serangan: {serangan}");
    }
}

// Interface untuk Kemampuan
interface IKemampuan
{
    string Nama { get; }
    int Cooldown { get; set; }
    void Aktifkan(Robot target);
}

// Class Kemampuan Perbaikan
class Repair : IKemampuan
{
    public string Nama => "Perbaikan";
    public int Cooldown { get; set; } = 0;

    public void Aktifkan(Robot target)
    {
        if (Cooldown == 0)
        {
            target.energi += 20;
            Console.WriteLine($"{target.nama} menggunakan {Nama} dan memulihkan 20 energi!");
            Cooldown = 3;  // Set cooldown
        }
        else
        {
            Console.WriteLine($"{Nama} belum bisa digunakan, masih cooldown.");
        }
    }
}

// Class Kemampuan Electric Shock
class ElectricShock : IKemampuan
{
    public string Nama => "Electric Shock";
    public int Cooldown { get; set; } = 0;

    public void Aktifkan(Robot target)
    {
        if (Cooldown == 0)
        {
            target.energi -= 30;
            Console.WriteLine($"{target.nama} terkena {Nama}, menyebabkan 30 damage!");
            Cooldown = 4;  // Set cooldown
        }
        else
        {
            Console.WriteLine($"{Nama} belum bisa digunakan, masih cooldown.");
        }
    }
}

// Class Kemampuan Plasma Cannon
class PlasmaCannon : IKemampuan
{
    public string Nama => "Plasma Cannon";
    public int Cooldown { get; set; } = 0;

    public void Aktifkan(Robot target)
    {
        if (Cooldown == 0)
        {
            int damage = 50;  // Serangan besar, menembus armor
            target.energi -= damage;
            Console.WriteLine($"{target.nama} terkena {Nama}, menyebabkan {damage} damage yang menembus armor!");
            Cooldown = 5;  // Set cooldown
        }
        else
        {
            Console.WriteLine($"{Nama} belum bisa digunakan, masih cooldown.");
        }
    }
}

// Class Kemampuan Super Shield
class SuperShield : IKemampuan
{
    public string Nama => "Pertahanan Super";
    public int Cooldown { get; set; } = 0;

    public void Aktifkan(Robot target)
    {
        if (Cooldown == 0)
        {
            target.armor += 20;  // Meningkatkan armor sementara
            Console.WriteLine($"{target.nama} menggunakan {Nama}, armor bertambah 20 untuk sementara!");
            Cooldown = 3;  // Set cooldown
        }
        else
        {
            Console.WriteLine($"{Nama} belum bisa digunakan, masih cooldown.");
        }
    }
}

// Class Robot biasa
class RobotBiasa : Robot
{
    public RobotBiasa(string nama, int energi, int armor, int serangan) : base(nama, energi, armor, serangan)
    {
    }

    public override void GunakanKemampuan(IKemampuan kemampuan, Robot target)
    {
        kemampuan.Aktifkan(target);
    }
}

// Class Bos Robot
class BosRobot : Robot
{
    public BosRobot(string nama, int energi, int armor, int serangan) : base(nama, energi, armor, serangan)
    {
    }

    // Method menerima serangan
    public void Diserang(Robot penyerang)
    {
        int damage = penyerang.serangan - this.armor;  // Mengurangi damage dengan armor target
        if (damage < 0)
        {
            damage = 0;  // Jika damage negatif, set menjadi 0
        }
        this.energi -= damage;
        Console.WriteLine($"{penyerang.nama} menyerang Bos {this.nama}, menyebabkan {damage} damage!");

        if (this.energi <= 0)
        {
            Mati();
        }
    }

    public override void GunakanKemampuan(IKemampuan kemampuan, Robot target)
    {
        kemampuan.Aktifkan(target);
    }

    // Method mati
    public void Mati()
    {
        Console.WriteLine($"Bos {this.nama} telah dikalahkan!");
    }
}


