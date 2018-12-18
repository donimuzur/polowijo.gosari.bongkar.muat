using AutoMapper;
using polowijo.gosari.bongkar.muat.Model.Master;
using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.bongkar.muat.Mapper
{
    public class CustomMapper
    {
        public CustomMapper()
        {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<PekerjaModel, MasterPetugasDto>().ReverseMap();
                cfg.CreateMap<MasterPetugasDto, master_petugas>().ReverseMap();

                cfg.CreateMap<SupplierModel, MasterSupplierDto>().ReverseMap();
                cfg.CreateMap<MasterSupplierDto, master_supplier>().ReverseMap();

                cfg.CreateMap<ItemModel, MasterItemDto>().ReverseMap();
                cfg.CreateMap<MasterItemDto, master_item>().ReverseMap();

                cfg.CreateMap<TRNBongkarMuatModel, TRNBongkarMuatDto>().ReverseMap();
                cfg.CreateMap<TRNBongkarMuatDto, trn_bongkar_muat>().ReverseMap();

                cfg.CreateMap<TRNBongkarMuatDetailsPekerjaModel, TRNBongkarMuatDetailsPekerjaDto>().ReverseMap();
                cfg.CreateMap<TRNBongkarMuatDetailsPekerjaDto, trn_bongkat_muat_details_pekerja>().ReverseMap();

                cfg.CreateMap<MasterPetugasDto, TRNBongkarMuatDetailsPekerjaDto>()                
                .ForMember(dest => dest.ID_PEKERJA, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.NAMA_PEKERJA, opt => opt.MapFrom(src => src.NAMA_PETUGAS));
            });
        }
    }
}
public static class MappingExpressionExtensions
{
    public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
    {
        expression.ForAllMembers(opt => opt.Ignore());
        return expression;
    }
}
