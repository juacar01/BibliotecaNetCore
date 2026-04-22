using AutoMapper;
using Biblioteca.Application.Features.Authors.Commands.CreateAuthor;
using Biblioteca.Application.Features.Authors.Queries.Vms;
using Biblioteca.Application.Features.Books.Commands.CreateBook;
using Biblioteca.Application.Features.Books.Queries.Vms;
using Biblioteca.Domain;

namespace Biblioteca.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Aquí puedes configurar tus mapeos
        // CreateMap<Source, Destination>();

        CreateMap<Author, AuthorVm>();
        CreateMap<Book, BookVm>();

        CreateMap<CreateBookCommand, Book>();
        CreateMap<CreateAuthorCommand, Author>();

    }
}
