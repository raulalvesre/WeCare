using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace WeCare.Application.Validators;

public class ImageValidator : AbstractValidator<IFormFile>
{
    private readonly long _fileSizeLimit = 1048576;
    private readonly Dictionary<string, List<byte[]>> _fileSignature =
        new()
        {
            {
                ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            {
                ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            },
            {
                ".png", new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
            }
        };
    
    public ImageValidator()
    {
        RuleFor(x => x)
            .Must(HasValidExtension)
            .WithMessage("Arquivo com extensão invalida")
            .DependentRules(() =>
                RuleFor(x => x)
                    .Must(HasValidSize)
                    .WithMessage($"Arquivo com tamanho maior que {_fileSizeLimit / 1024 / 1024}mb")
                    .Must(HasValidSignature)
                    .WithMessage("Arquivo com assinatura errada"));
    }

    private bool HasValidExtension(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return !string.IsNullOrEmpty(fileExtension) && _fileSignature.Any(x => x.Key.Equals(fileExtension));
    }

    private bool HasValidSize(IFormFile file)
    {
        return file.Length < _fileSizeLimit;
    }

    private bool HasValidSignature(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        
        using var reader = new BinaryReader(ms);
        reader.BaseStream.Position = 0;
        
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var signatures = _fileSignature[fileExtension];
        var headerBytes =  reader.ReadBytes(signatures.Max(m => m.Length));

        return signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
    }
    
}