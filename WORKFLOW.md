# Git Workflow

## Giới thiệu
Đây là mô tả về quy trình làm việc cơ bản với Git để quản lý mã nguồn trong dự án.

## Bước 1: Clone Repository (Chỉ thực hiện 1 lần duy nhất, những lần push code sau sẽ thực hiện từ Bước 3)
Clone repository từ remote về local để bắt đầu làm việc.

```bash
git clone https://github.com/caochanhkhai1510/Health_Insurance.git
```

## Bước 2: Chuyển sang Branch của cá nhân (Mỗi khi bắt đầu code thì nên thực hiện lại bước này)
Chuyển sang branch cá nhân (chứa tên bản thân) để bắt đầu làm việc 

```bash
git checkout Thinh/Tien/Tuyet/Nam/Khai
```

## Bước 3: Commit
Sau khi thực hiện thay đổi về code trong branch cá nhân, hãy commit chúng mỗi khi hoàn thành.

```bash
git add .
git commit -m "Nội dung commit"
```

## Bước 4: Cập nhật thay đổi từ branch Main
Trước khi push lên branch cá nhân, hãy thực hiện fetch để cập nhật thông tin về những thay đổi từ branch Main. Sau đó, thực hiện merge để cập nhật những thay đổi đó lên branch cá nhân.

```bash
git fetch origin main
git merge origin/main
```

## Bước 5: Push code
Bắt đầu push code của cá nhân lên branch cá nhân

```bash
git push origin Thinh/Tien/Tuyet/Nam/Khai
```



