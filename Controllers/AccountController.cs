using BankingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Cryptography.Xml;

namespace BankingApplication.Controllers
{
    public class AccountController : Controller
    {
        #region Constructor
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Accounts Page
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accounts = await _context.Accounts.OrderByDescending(x => x.Balance).ToListAsync();
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Name, decimal Balance)
        {
            var account = new Account
            {
                Name = Name,
                Balance = Balance
            };
            _context.Add(account);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var account = _context.Accounts
                .Where(a => a.AccountID == id)
                .FirstOrDefault();

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int AccountID, string Name, decimal Balance)
        {
            var account = _context.Accounts
                .Where(a => a.AccountID == AccountID)
                .FirstOrDefault();

            account.Name = Name;
            account.Balance = Balance;

            _context.Update(account);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Transfer Funds
        [HttpGet]
        public IActionResult TransferFunds()
        {
            var accounts = _context.Accounts
                .Select(a => new SelectListItem
                {
                    Value = a.AccountID.ToString(),
                    Text = a.Name
                })
                .ToList();
            ViewBag.Accounts = accounts;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TransferFunds(int FromAccount, int ToAccount, decimal Amount)
        {
            var fromAccount = _context.Accounts
                .Where(a => a.AccountID == FromAccount)
                .FirstOrDefault();

            var toAccount = _context.Accounts
                .Where(a => a.AccountID == ToAccount)
                .FirstOrDefault();


            fromAccount.Balance = fromAccount.Balance - Amount;
            toAccount.Balance = toAccount.Balance + Amount;


            _context.Update(fromAccount);
            _context.Update(toAccount);

            var transaction = new Transaction
            {
                FromAccount = FromAccount,
                ToAccount = ToAccount,
                Amount = Amount,
                TransactionTime = DateTime.Now,
                FromAccountBalance = fromAccount.Balance,
                ToAccountBalance = toAccount.Balance
            };

            _context.Add(transaction);

            await _context.SaveChangesAsync();

            var accounts = _context.Accounts
                .Select(a => new SelectListItem
                {
                    Value = a.AccountID.ToString(),
                    Text = a.Name
                })
                .ToList();

            ViewBag.Accounts = accounts;

            return View();
        }
        #endregion

        #region Transactions
        [HttpGet]
        public async Task<IActionResult> Transactions()
        {
            var transactions = await _context.Transactions
                .OrderByDescending(t => t.TransactionTime)
                .ToListAsync();

            return View(transactions);
        }
        #endregion
    }
}
