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
            });
        }
    }
}
