﻿using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi.Infraestructure.Data;

namespace Infraestructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly AppDbContext _context;
		protected readonly DbSet<T> _dbSet;

		public Repository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

		public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

		public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

		public void Update(T entity) => _dbSet.Update(entity);

		public void Delete(T entity) => _dbSet.Remove(entity);
	}
}
