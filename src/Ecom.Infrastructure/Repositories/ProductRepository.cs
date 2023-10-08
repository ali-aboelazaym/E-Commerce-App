using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        //private readonly IUnitOfWork _uow;

        public ProductRepository(ApplicationDbContext context ,IFileProvider fileProvider,IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
            //_uow = uow;
        }

        public async Task<bool> AddAsync(CreateProductDto dto)
        {
            if (dto.Image is not null)
            {
                var root = "/images/products/";
                var ProductName= $"{Guid.NewGuid()}"+dto.Image.FileName;
                if (!Directory.Exists("wwwroot"+root))
                {
                    Directory.CreateDirectory("wwwroot" + root);
                }
                var src = root + ProductName;
                var PicInfo=_fileProvider.GetFileInfo(src);
                var rootpath = PicInfo.PhysicalPath;

                using (var fileStream = new FileStream(rootpath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(fileStream);
                }

                //Create new product

                var res = _mapper.Map<Product>(dto);
                res.ProductPicture = rootpath;
                await _context.Products.AddAsync(res);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
