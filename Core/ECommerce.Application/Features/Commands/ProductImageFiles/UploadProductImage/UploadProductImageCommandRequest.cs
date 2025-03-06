using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Commands.ProductImageFiles.UploadProductImage
{
    public class UploadProductImageCommandRequest:IRequest<UploadProductImageCommandResponse>
    {
        public  string Id { get; set; }
    }
}
