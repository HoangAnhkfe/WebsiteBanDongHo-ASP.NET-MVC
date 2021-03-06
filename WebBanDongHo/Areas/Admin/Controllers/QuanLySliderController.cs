﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "QUANTRI")]
    public class QuanLySliderController : Controller
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLySlider
        public ActionResult DanhSachSlider(int? page)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listSliders = db.Sliders.ToList();
            return View(listSliders.OrderBy(n => n.MaSlider).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CapNhatSlider()
        {
            List<Slider> LS = db.Sliders.ToList();
            if(LS.Count() > 0)
            {
                return View(LS);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult CapNhatSlider(String listAnh)
        {
            List<Slider> LS = db.Sliders.ToList();
            if(LS != null)
            {
                foreach (Slider item in LS)
                {
                    db.Sliders.Remove(item);
                    db.SaveChanges();
                }
            }
            
            if (listAnh != "")
            {
                string[] arrListStr = listAnh.Split(',');
                foreach (String item in arrListStr)
                {
                    Slider slider = new Slider();
                    slider.Anh = item.Substring(23);
                    db.Sliders.Add(slider);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("DanhSachSlider");
        }
    }
}