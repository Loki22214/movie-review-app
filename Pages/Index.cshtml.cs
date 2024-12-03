using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Models;

namespace MovieReviewApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;
    private string SortOrder {get; set;}

    public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

     public IList<Movie> Movies { get;set; } = default!;

    public void OnGet(string sortOrder)
    {
       // Store the sort order parameter
        // Store the sort order parameter
        SortOrder = sortOrder;

        // Retrieve all movies from the database
        Movies = _context.Movie.ToList();

        // Perform sorting in memory
        switch (sortOrder?.ToLower())
        {
            case "highestrating":
                Movies = Movies.OrderByDescending(m => m.Rating).ToList();
                break;
            case "lowestrating":
                Movies = Movies.OrderBy(m => m.Rating).ToList();
                break;
            default:
                Movies = Movies.OrderBy(m => m.Title).ToList(); // Default sorting by title
                break;
        }
    }
}
