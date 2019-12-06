using DataCore.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Admin.Documents;
using WebApi.Models.Admin.Documents.IncomingDocuments;
using WebApi.Models.Admin.Documents.OutcomingDocuments;

namespace WebApi.Mappings.Documents
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentMappings : AutoMapper.Profile
    {
        /// <summary>
        /// ctor
        /// </summary>
        public DocumentMappings()
        {
            CreateMap<IncomingDocument, IncomingDocumentDetailDto>()
                .ForMember(x => x.Author, cfg => cfg.MapFrom(z => new Lookup()
                {
                    Id = z.Author.Id,
                    Title = $"{z.Author.FullName} {z.Author.UserName}"
                }))
                .ForMember(x => x.Entries, cfg => cfg.MapFrom(z => z.Entries.Select(x => new DocumentEntryDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Count = x.Count,
                    Price = x.Price,
                    DocumentId = x.DocumentId,
                    Product = new Lookup
                    {
                        Id = x.Product.Id,
                        Title = x.Product.Title
                    }
                })));

            CreateMap<IncomingDocumentDetailDto, IncomingDocument>()
                .ForMember(x => x.Author, cfg => cfg.Ignore())
                .ForMember(x => x.Shipper, opts => opts.Ignore())
                .ForMember(x => x.Entries, cfg => cfg.MapFrom(z => z.Entries.Select(x => new IncomingDocumentEntry
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Count = x.Count,
                    Price = x.Price,
                    DocumentId = x.DocumentId,
                })));

            CreateMap<IncomingDocument, IncomingDocumentListDto>()
                .ForMember(x => x.Author, cfg => cfg.MapFrom(z => new Lookup()
                {
                    Id = z.Author.Id,
                    Title = $"{z.Author.FullName} {z.Author.UserName}"
                }));

            CreateMap<OutComingDocument, OutcomingDocumentDetailDto>()
                .ForMember(x => x.Author, cfg => cfg.MapFrom(z => new Lookup()
                {
                    Id = z.Author.Id,
                    Title = z.Author.FullName
                }))
                .ForMember(x => x.Entries, cfg => cfg.MapFrom(z => z.Entries.Select(x => new DocumentEntryDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Count = x.Count,
                    DocumentId = x.DocumentId,
                    Product = new Lookup
                    {
                        Id = x.Product.Id,
                        Title = x.Product.Title
                    }
                })));

            CreateMap<OutcomingDocumentDetailDto, OutComingDocument>()
                .ForMember(x => x.Author, cfg => cfg.Ignore())
                .ForMember(x => x.Entries, cfg => cfg.MapFrom(z => z.Entries.Select(x => new OutComingDocumentEntry
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Count = x.Count,
                    DocumentId = x.DocumentId,
                })));

            CreateMap<OutComingDocument, OutcomingDocumentListDto>()
                .ForMember(x => x.Author, cfg => cfg.MapFrom(z => new Lookup()
                {
                    Id = z.Author.Id,
                    Title = z.Author.FullName
                }));

        }
    }
}
