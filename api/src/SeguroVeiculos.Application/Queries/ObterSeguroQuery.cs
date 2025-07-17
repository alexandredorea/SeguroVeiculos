using MediatR;
using SeguroVeiculos.Application.DTOs;

namespace SeguroVeiculos.Application.Queries;

public class ObterSeguroQuery : IRequest<SeguroDto?>
{
    public int Id { get; set; }

    public ObterSeguroQuery(int id)
    {
        Id = id;
    }
}

