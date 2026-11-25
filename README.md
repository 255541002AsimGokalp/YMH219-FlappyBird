# YMH219 - Flappy Bird Masaüstü Oyunu

## Proje Açıklaması
Nesne Tabanlı Programlama (YMH 219) dersi kapsamında C# ve .NET Windows Forms kullanılarak geliştirilmiş, zamanlayıcı (Timer) tabanlı Flappy Bird benzeri masaüstü oyunudur. Kuş, boru engelleri, skor yönetimi ve oyun motoru ayrı sınıflar halinde nesne tabanlı olarak tasarlanmıştır.

## Kullanılan Teknoloji ve Paketler
- C# / .NET 8.0 (Windows Forms)
- Visual Studio 2022
- System.Drawing (GDI+ Double-Buffered çizim)

## Kurulum ve Çalıştırma Adımları
1. Depoyu bilgisayarınıza klonlayın:
   ```bash
   git clone https://github.com/255541002AsimGokalp/YMH219-FlappyBird.git
   ```
2. `YMH219-FlappyBird.sln` dosyasını Visual Studio 2022 ile açın.
3. `F5` tuşuna basarak veya `dotnet run` komutuyla uygulamayı çalıştırın.

## Uygulamanın Temel Özellikleri
- `Space` / `Yukarı Ok` tuşu veya fare tıklaması ile zıplama ve yer çekimi fiziği
- Ekran dışına çıktıkça rastgele yüksekliklerle yeniden konumlanan hareketli boru engelleri
- Çarpışma kontrolü ve Game Over ekranı
- Geçilen her engel için anlık skor artışı ve her 5 puanda zorluk (hız) artışı
- En yüksek skorun yerel `highscore.txt` dosyasında saklanması
- `R` tuşu veya buton ile hızlı yeniden başlatma

## Ekran Görüntüleri
![Oyun İçi Ekran Görüntüsü](screenshots/screenshot1.png)
![Game Over Ekran Görüntüsü](screenshots/screenshot2.png)

## Bilinen Sınırlamalar / Geliştirme Notları
- Uygulama Windows Forms tabanlı olduğu için Windows işletim sisteminde çalıştırılmalıdır.
- İleri sürümlerde ses efektleri ve farklı zorluk modları eklenebilir.

## Öğrenci Bilgileri
- **Ad-Soyad:** Asım Gökalp
- **Öğrenci Numarası:** 255541002
