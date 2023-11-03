using AutoMapper;
using AutoMapper.QueryableExtensions;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<TResult> Add<TSource, TResult>(TSource source)
        {
            var genericEntity = _mapper.Map<T>(source);
            await _context.AddAsync(genericEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TResult>(genericEntity);
        }

        public async Task Delete(int? id)
        {

            var entity = await _context.FindAsync<T>(id);
            if (entity is null)
            {
                throw new Exception();
            }

            _context.Remove(entity);
            _context.SaveChanges();

        }

        public async Task<bool> Exists(int id)
        {
            return await _context.FindAsync<T>(id) is not null;
        }

        public async Task<TResult> Get<TResult,TSource>(TSource id)
        {
            var entity = await _context.FindAsync<T>(id);
            if (entity is null)
            {
                throw new Exception();
            }

            return _mapper.Map<TResult>(entity);
        }

        public async Task<TResult> Get<TResult>(int id)
        {
            var entity = await _context.FindAsync<T>(id);
            if (entity is null)
            {
                throw new Exception();
            }

            return _mapper.Map<TResult>(entity);

        }

        public async Task<IEnumerable<TResult>> GetAll<TResult>()
        {
           return _context.Set<T>()
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .ToList();
        }

        public async Task Update<TSource>(int id, TSource updateEntity)
        {
            var orignalEntity = await _context.FindAsync<T>(id);
            if (orignalEntity is null)
            {
                throw new Exception();
            }
            _mapper.Map(updateEntity, orignalEntity);
            _context.Update(orignalEntity);
            _context.SaveChanges();

        }

    }
}
