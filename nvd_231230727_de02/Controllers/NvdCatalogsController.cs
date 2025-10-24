using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nvd_231230727_de02.Data;
using nvd_231230727_de02.Models;

namespace nvd_231230727_de02.Controllers
{
    public class NvdCatalogsController : Controller
    {
        private readonly Ngovandung231230727De02Context _context;
        private readonly IWebHostEnvironment _environment;

        public NvdCatalogsController(Ngovandung231230727De02Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: NvdCatalogs
        public async Task<IActionResult> nvdIndex()
        {
            return View(await _context.NvdCatalogs.ToListAsync());
        }

        // GET: NvdCatalogs/Details/5
        public async Task<IActionResult> nvdDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvdCatalog = await _context.NvdCatalogs
                .FirstOrDefaultAsync(m => m.NvdId == id);
            if (nvdCatalog == null)
            {
                return NotFound();
            }

            return View(nvdCatalog);
        }

        // GET: NvdCatalogs/Create
        public IActionResult nvdCreate()
        {
            return View();
        }

        // POST: NvdCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> nvdCreate([Bind("NvdId,NvdCateName,NvdCatePrice,NvdCatePrice,NvdCateQty,NvdCateActive")] NvdCatalog nvdCatalog, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                //load ảnh
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string upLoadsFolder = Path.Combine(_environment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(upLoadsFolder, uniqueFileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }
                    nvdCatalog.NvdPicture = uniqueFileName;
                }
                //
                _context.Add(nvdCatalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(nvdIndex));
            }
            return View(nvdCatalog);
        }

        // GET: NvdCatalogs/Edit/5
        public async Task<IActionResult> nvdEdit(int? nvdid)
        {
            if (nvdid == null)
            {
                return NotFound();
            }

            var nvdCatalog = await _context.NvdCatalogs.FindAsync(nvdid);
            if (nvdCatalog == null)
            {
                return NotFound();
            }
            return View(nvdCatalog);
        }

        // POST: NvdCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> nvdEdit(int nvdid, [Bind("NvdId,NvdCateName,NvdCatePrice,NvdCateQty,NvdPicture,NvdCateActive")] NvdCatalog nvdCatalog, IFormFile? ImageFile)
        {
            if (nvdid != nvdCatalog.NvdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //cập nhật ảnh
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        // 1. Xóa file cũ (nếu có)
                        // Dùng nvdSanPham.nvdHinhAnh từ hidden input để lấy tên file cũ
                        if (!string.IsNullOrEmpty(nvdCatalog.NvdPicture))
                        {
                            string oldImagePath = Path.Combine(_environment.WebRootPath, "images", nvdCatalog.NvdPicture);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // 2. Lưu file mới
                        string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }

                        // 3. Cập nhật tên file mới vào model để lưu vào DB
                        nvdCatalog.NvdPicture = uniqueFileName;
                    }
                    //
                    _context.Update(nvdCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvdCatalogExists(nvdCatalog.NvdId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(nvdIndex));
            }
            return View(nvdCatalog);
        }

        // GET: NvdCatalogs/Delete/5
        public async Task<IActionResult> nvdDelete(int? nvdid)
        {
            if (nvdid == null)
            {
                return NotFound();
            }

            var nvdCatalog = await _context.NvdCatalogs
                .FirstOrDefaultAsync(m => m.NvdId == nvdid);
            if (nvdCatalog == null)
            {
                return NotFound();
            }

            return View(nvdCatalog);
        }

        // POST: NvdCatalogs/Delete/5
        [HttpPost, ActionName("nvdDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int nvdid)
        {
            var nvdCatalog = await _context.NvdCatalogs.FindAsync(nvdid);
            if (nvdCatalog != null)
            {
                _context.NvdCatalogs.Remove(nvdCatalog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(nvdIndex));
        }

        private bool NvdCatalogExists(int nvdid)
        {
            return _context.NvdCatalogs.Any(e => e.NvdId == nvdid);
        }
    }
}
