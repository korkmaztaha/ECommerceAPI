using ECommerceApi.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Lütfen ürün ismi giriniz")
                .NotNull().WithMessage("Lütfen ürün ismi giriniz")
                .MaximumLength(150)
                .MinimumLength(3);

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen Stok giriniz")
                .Must(s => s >= 0)
                .WithMessage("Stok negatif olamaz");

            RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
                .WithMessage("Lütfen Fiyat giriniz")
            .Must(p => p >= 0)
            .WithMessage("Fiyat negatif olamaz");


        }
    }
}
