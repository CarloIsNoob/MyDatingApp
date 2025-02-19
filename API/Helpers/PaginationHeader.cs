using System;

namespace API.Helpers;

public class PaginationHeader(int CurrentPage, int itemsPerPage, int TotalItems, int totalPages)
{
    public int CurrentPage { get; set; } = CurrentPage;

    public int ItemsPerPage { get; set; } = itemsPerPage;

    public int TotalItems { get; set; } = TotalItems;

    public int TotalPages { get; set; } = totalPages;
}
