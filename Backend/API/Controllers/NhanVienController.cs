using API.Data;
using API.Domain;
using API.DTOs;
using API.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public NhanVienController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dsnv = VHIDbContext.NhanVien.ToList();
            if(dsnv.Count() == 0 || dsnv == null)
            {
                return NotFound("Không tồn tại nhân viên");
            }
            List<NhanVienDTO> dsnvDTO = new List<NhanVienDTO>();
            foreach (var nv in dsnv)
            {
                NhanVienDTO nv_dto = CreateNhanVienDTO(nv);
                dsnvDTO.Add(nv_dto);
            }
            return Ok(dsnvDTO);
        }

        [HttpGet]
        [Route("GetAllNV")]
        public IActionResult GetAllNV()
        {
            var dsnv = VHIDbContext.NhanVien.Where(x=>x.LoaiNhanVien == "Nhân Viên").ToList();
            if (dsnv.Count() == 0 || dsnv == null)
            {
                return NotFound("Không tồn tại nhân viên");
            }
            List<NhanVienDTO> dsnvDTO = new List<NhanVienDTO>();
            foreach (var nv in dsnv)
            {
                NhanVienDTO nv_dto = CreateNhanVienDTO(nv);
                dsnvDTO.Add(nv_dto);
            }
            return Ok(dsnvDTO);
        }

        [HttpGet]
        [Route("GetAllNVTC")]
        public IActionResult GetAllNVTC()
        {
            var dsnv = VHIDbContext.NhanVien.Where(x => x.LoaiNhanVien == "Nhân Viên Tài Chính").ToList();
            if (dsnv.Count() == 0 || dsnv == null)
            {
                return NotFound("Không tồn tại nhân viên");
            }
            List<NhanVienDTO> dsnvDTO = new List<NhanVienDTO>();
            foreach (var nv in dsnv)
            {
                NhanVienDTO nv_dto = CreateNhanVienDTO(nv);
                dsnvDTO.Add(nv_dto);
            }
            return Ok(dsnvDTO);
        }

        [HttpGet]
        [Route("GetAllAdmin")]
        public IActionResult GetAllAdmin()
        {
            var dsnv = VHIDbContext.NhanVien.Where(x => x.LoaiNhanVien == "Quản Trị Viên").ToList();
            if (dsnv.Count() == 0 || dsnv == null)
            {
                return NotFound("Không tồn tại nhân viên");
            }
            List<NhanVienDTO> dsnvDTO = new List<NhanVienDTO>();
            foreach (var nv in dsnv)
            {
                NhanVienDTO nv_dto = CreateNhanVienDTO(nv);
                dsnvDTO.Add(nv_dto);
            }
            return Ok(dsnvDTO);
        }

        [HttpGet]
        [Route("id:int")]
        public IActionResult GetById(int id)
        {
            var nv = VHIDbContext.NhanVien.FirstOrDefault(x => x.ID_NhanVien == id);
            if (nv == null)
            {
                return NotFound();
            }
            var nv_dto = CreateNhanVienDTO(nv);
            return Ok(nv_dto);
        }

        [HttpGet]
        [Route("GetByIdNhanVien1")]
        public IActionResult GetByIdtk(int idtk)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == idtk);

            if (tk == null)
            {
                return NotFound("Không tồn tại Tài khoản.");
            }

            var dsnv = VHIDbContext.NhanVien.Where(q => q.TaiKhoanID_TaiKhoan == idtk).ToList();

            if (dsnv == null || dsnv.Count() == 0)
            {
                return NotFound("Không tìm thấy Tài khoản tương ứng với Nhân viên.");
            }

            List<NhanVienDTO> dsnvDTO = new List<NhanVienDTO>();
            foreach (var nv in dsnv)
            {
                NhanVienDTO dsnv_dto = CreateNhanVienDTO(nv);
                dsnvDTO.Add(dsnv_dto);
            }

            return Ok(dsnvDTO);
        }

        [HttpPost]
        [Route("ThemNhanVien")]
        public IActionResult ThemNhanVien([FromBody] AddNhanVienDTO dto)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == dto.ID_TaiKhoan);
            if (tk == null)
            {
                return NotFound("Không tìm thấy Tài khoản");
            }
            if (dto.GioiTinh != "Nữ" && dto.GioiTinh != "Nam")
            {
                return BadRequest("Giới tính không hợp lệ");
            }
            if (dto.LoaiNhanVien != "Nhân Viên" && dto.LoaiNhanVien != "Nhân Viên Tài Chính" && dto.LoaiNhanVien != "Quản Trị Viên")
            {
                return BadRequest("Loại nhân viên không hợp lệ");
            }
            NhanVien nv_Domain = CreateNhanVienDomain(dto, tk);

            VHIDbContext.NhanVien.Add(nv_Domain);
            VHIDbContext.SaveChanges();

            NhanVienDTO nvDTO = CreateNhanVienDTO(nv_Domain);

            return Ok(nvDTO);
        }

        [HttpPut("ChinhSuaLoaiNhanVien")]
        public IActionResult ChinhSuaLoaiNhanVien([FromBody] UpdateLoaiNhanVienDTO dto)
        {
            var nv = VHIDbContext.NhanVien.FirstOrDefault(x => x.ID_NhanVien == dto.id);
            if (nv == null)
            {
                return NotFound("Không tồn tại nhân viên.");
            }
            //Kiểm tra loại nhân viên
            if (dto.LoaiNhanVien != "Nhân Viên" && dto.LoaiNhanVien != "Nhân Viên Tài Chính" && dto.LoaiNhanVien != "Quản Trị Viên")
            {
                return BadRequest("Loại nhân viên không hợp lệ");
            }
            // Cập nhật và lưu vào cơ sở dữ liệu
            nv.LoaiNhanVien = dto.LoaiNhanVien;
            VHIDbContext.SaveChanges();

            NhanVienDTO nv_dto = CreateNhanVienDTO(nv);

            return Ok(nv_dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult XoaNhanVien([FromRoute] int id)
        {
            var nv = VHIDbContext.NhanVien.FirstOrDefault(x => x.ID_NhanVien == id);
            if (nv == null)
            {
                return NotFound("Không tồn tại nhân viên.");
            }
            VHIDbContext.NhanVien.Remove(nv);
            VHIDbContext.SaveChanges();

            NhanVienDTO nv_dto = CreateNhanVienDTO(nv);

            return Ok(nv_dto);
        }

        private static NhanVien CreateNhanVienDomain(AddNhanVienDTO dto, TaiKhoan? tk)
        {
            return new NhanVien()
            {
                HoTen = dto.HoTen,
                GioiTinh = dto.GioiTinh,
                QuocTich = dto.QuocTich,
                NgaySinh = dto.NgaySinh,
                CMND = dto.CMND,
                SoNhaTenDuong = dto.SoNhaTenDuong,
                PhuongXa = dto.PhuongXa,
                QuanHuyen = dto.QuanHuyen,
                ThanhPho = dto.ThanhPho,
                Email = dto.Email,
                LoaiNhanVien = dto.LoaiNhanVien,
                SoTaiKhoan = dto.SoTaiKhoan,
                NganHang = dto.NganHang,
                SoDienThoai = dto.SoDienThoai,
                TaiKhoan = tk
            };
        }

        private static NhanVienDTO CreateNhanVienDTO(NhanVien? nv)
        {
            return new NhanVienDTO()
            {
                ID_NhanVien = nv.ID_NhanVien,
                HoTen = nv.HoTen,
                GioiTinh = nv.GioiTinh,
                QuocTich = nv.QuocTich,
                NgaySinh = nv.NgaySinh,
                CMND = nv.CMND,
                SoNhaTenDuong = nv.SoNhaTenDuong,
                PhuongXa = nv.PhuongXa,
                QuanHuyen = nv.QuanHuyen,
                ThanhPho = nv.ThanhPho,
                Email = nv.Email,
                LoaiNhanVien = nv.LoaiNhanVien,
                SoTaiKhoan = nv.SoTaiKhoan,
                NganHang = nv.NganHang,
                SoDienThoai = nv.SoDienThoai,
                ID_TaiKhoan = nv.TaiKhoanID_TaiKhoan
            };
        }
    }

}
