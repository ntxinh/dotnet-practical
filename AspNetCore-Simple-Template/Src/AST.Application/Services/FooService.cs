using AST.Core.Entities;
using AST.Core.Interfaces;
using AST.Application.Interfaces;
using System.Collections.Generic;
using AST.Application.ViewModels;
using System.Threading.Tasks;
using AST.Core.Common.ResponseBuilder;
using System;

namespace AST.Application.Services
{
    public class FooService: IFooService
    {
        private readonly IRepository<Foo> _fooRepository;

        public FooService(IRepository<Foo> fooRepository)
        {
            _fooRepository = fooRepository;
        }

        ApiResponse<IEnumerable<Foo>> IFooService.GetAll()
        {
            var res = new ApiResponseBuilder<IEnumerable<Foo>>();
            try
            {
                var data = _fooRepository.GetAll();
                return res.Success(data).Build();
            }
            catch (Exception ex)
            {
                return res.Error().DebugMessage(ex.Message).Build();
            }
        }

        public async Task<ApiResponse<Foo>> GetById(int id)
        {
            var res = new ApiResponseBuilder<Foo>();
            try
            {
                var data = await _fooRepository.GetAsync(id);
                return res.Success(data).Build();
            }
            catch (Exception ex)
            {
                return res.Error().DebugMessage(ex.Message).Build();
            }
        }

        public async Task<ApiResponse<Foo>> Add(FooFormModel model)
        {
            var res = new ApiResponseBuilder<Foo>();
            try
            {
                var data = await _fooRepository.AddAsync(new Foo
                {
                    Bar = model.Bar,
                    FooBar = model.FooBar,
                });
                return res.Success(data).Build();
            }
            catch (Exception ex)
            {
                return res.Error().DebugMessage(ex.Message).Build();
            }
        }

        public async Task<ApiResponse<bool>> UpdateById(int id, FooFormModel model)
        {
            var res = new ApiResponseBuilder<bool>();
            try
            {
                var foo = await _fooRepository.GetAsync(id);
                foo.Bar = model.Bar;
                foo.FooBar = model.FooBar;
                var data = await _fooRepository.UpdateAsync(foo);
                return res.Success(data > 0).Build();
            }
            catch (Exception ex)
            {
                return res.Error().DebugMessage(ex.Message).Build();
            }
        }

        public async Task<ApiResponse<bool>> DeleteById(int id)
        {
            var res = new ApiResponseBuilder<bool>();
            try
            {
                var data = await _fooRepository.DeleteAsync(id);
                return res.Success(data > 0).Build();
            }
            catch (Exception ex)
            {
                return res.Error().DebugMessage(ex.Message).Build();
            }
        }
    }
}
