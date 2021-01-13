using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Uf;

namespace src.Api.Service.Test.Uf
{
    public class UfTestes
    {
        public Guid IdUf { get; set; }
        public string NomeUf { get; set; }
        public string SiglaUf { get; set; }

        public UfDto ufDto;
        public List<UfDto> listUfDto = new List<UfDto>();

        public UfTestes()
        {
            IdUf = Guid.NewGuid();
            NomeUf = Faker.Address.UsState();
            SiglaUf = Faker.Address.UsState().Substring(1, 3);

            ufDto = new UfDto()
            {
                Id = IdUf,
                Nome = NomeUf,
                Sigla = SiglaUf,
            };

            for(int i = 0; i < new Random().Next(3, 6); i++ )
            {
                listUfDto.Add(
                    new UfDto
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3),
                    }
                );
            }
        }
    }
}