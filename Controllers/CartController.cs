using BookStore.Responsitories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public CartController(ICartRepository cartRepo, ApplicationDbContext db, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepo = cartRepo;
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(bookId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        [Authorize]
        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await _cartRepo.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }
        public async Task<IActionResult> CheckOut()
        {
            var user = await _userManager.GetUserAsync(User);
            var cartItems = _db.CartDetail
                .Include(b => b.Book)
                .Include(b => b.ShoppingCart)
                .ThenInclude(u => u!.User)
                .Where(u => u.ShoppingCart!.UserId == user!.Id);
            decimal totalPrice = 0;
            foreach (var item in cartItems)
            {
                totalPrice += @item.Book.Price * @item.Quantity;
                _db.CartDetail.Remove(item);
            }
            Order order = new Order()
            {
                UserId = user!.Id,
                Total = totalPrice
            };
            _db.Add(order);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}