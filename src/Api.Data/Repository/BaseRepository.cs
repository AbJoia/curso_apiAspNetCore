using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Api.Data.Context;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces;

namespace src.Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context; 
        private DbSet<T> _dataSet;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if(result == null) return false;

                _dataSet.Remove(result); 
                await _context.SaveChangesAsync();
                return true;     
            }
            catch(Exception e)
            {
                throw e;   
            }
        }

        public async Task<T> InsertAsync(T item)
        {
           try
           {
               if(item.Id == Guid.Empty)
               {
                   item.Id = Guid.NewGuid();
               }

               item.CreateAt = DateTime.UtcNow;
               _dataSet.Add(item);
               await _context.SaveChangesAsync();
           }
           catch(Exception e)
           {
               throw e;
           }

           return item;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
               var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
               if(result == null) return null;
               item.UpdateAt = DateTime.UtcNow;
               item.CreateAt = result.CreateAt;
               _context.Entry(result).CurrentValues.SetValues(item);
               await _context.SaveChangesAsync();                    
            }
            catch(Exception e)
            {
                throw e;
            }

            return item;
        }

        public async Task<bool> ExistAsync (Guid id)
        {
            return await _dataSet.AnyAsync(p => p.Id.Equals(id));
        }
    }
}