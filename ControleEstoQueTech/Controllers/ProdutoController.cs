﻿using DataAcess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleEstoQueTech
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produto;
        private readonly AppDataContext _context;
        private readonly IFornecedoresRepository _fornecedor;
        private readonly ICategoriaRepository _categoria;
    
        //private readonly UnitOfWork _unitOfWork;
      
        public ProdutoController(IProdutoRepository produto, AppDataContext context, ICategoriaRepository categoria, IFornecedoresRepository fornecedores)
        {
            _categoria = categoria;
            _produto = produto;
            _context = context;
            _fornecedor = fornecedores;
         
        }
        public IActionResult Index()
        {
            var obj = new GeralVM()
            {
             Produtos =  _produto.GetAll(),
             Categorias = _categoria.GetAll()
            };

            ViewBag.data = _produto.GetAll();
            ViewBag.dialogData = _produto.GetAll();
          

            return View(obj);
        }
        [HttpPost]
        public  IActionResult AdicionarProduto(Produto produto)
        {
            _produto.Add(produto);
            _produto.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Cadastrar()
        {
            ViewBag.Fornecedor = _fornecedor.GetAll();
            //ViewBag.Estados = _produto.GetAll().Select(c=> new SelectListItem() { Text = c.Categoria.Descricao,Value=c.Categoria.Descricao});
            ViewBag.Categorias = _categoria.GetAll();
   
            return View();
        }
        public IActionResult Excluir(Produto produto)
        {
            _produto.Remove(produto);
            _produto.Save();
            return RedirectToAction("Index");
        }
      
        //public IActionResult Excluir()
        //{
         
        //    return Redirect("Excluir");
        //}

         public IActionResult Edit(Produto produto)
        {
            ViewBag.Fornecedor = _fornecedor.GetAll();
            //ViewBag.Estados = _produto.GetAll().Select(c=> new SelectListItem() { Text = c.Categoria.Descricao,Value=c.Categoria.Descricao});
            ViewBag.Categorias = _categoria.GetAll();
            //return View(_produto.Get(produto));

            GeralVM obj = new GeralVM
            {
                Categorias = _context.Categorias,
                Produtos = _context.Produtos,
                Produto = _produto.Get(produto),

            };

            return View(obj);
        }

        public IActionResult Info(Produto produto)
        {
            return View(_produto.GetId(produto));
        }
        [HttpPost]
         public IActionResult Atualizar(Produto produto)
         {
         
             _produto.Update(produto);
             _produto.Save();


             return RedirectToAction("Index");
         }

    

    }
}
