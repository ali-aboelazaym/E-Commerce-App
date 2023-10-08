using AutoMapper;
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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileprovider;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ICategoryRepository CategoryRepository { get;  }

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(ApplicationDbContext context,IFileProvider fileprovider,IMapper mapper)
        {
            _context = context;
            _fileprovider = fileprovider;
            _mapper = mapper;
            
            CategoryRepository =new CategoryRepository(_context);
            ProductRepository=new ProductRepository(_context,_fileprovider,_mapper);
        }
    }
}
