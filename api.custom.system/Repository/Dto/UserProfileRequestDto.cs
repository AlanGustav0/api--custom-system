﻿namespace api.custom.system.Repository.Dto
{
    public class UserProfileRequestDto
    {
        public int Id { get; set; }

        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Endereco { get; set; }
        public string? Numero { get; set; }
        public string? Cep { get; set; }

        public string? Cidade { get; set; }

        public string? Estado { get; set; }
    }
}
