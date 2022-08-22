// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App_Estacionamento.Areas.Identity.Pages
{
    /// <resumo>
    ///     Esta API oferece suporte � infraestrutura de interface do usu�rio padr�o do ASP.NET Core Identity e n�o se destina a ser usada
    ///     diretamente do seu c�digo. Esta API pode ser alterada ou removida em vers�es futuras.
    /// </summary>
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        /// <resumo>
        ///     Esta API oferece suporte � infraestrutura de interface do usu�rio padr�o do ASP.NET Core Identity e n�o se destina a ser usada
        ///     diretamente do seu c�digo. Esta API pode ser alterada ou removida em vers�es futuras.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        ///     Esta API oferece suporte � infraestrutura de interface do usu�rio padr�o do ASP.NET Core Identity e n�o se destina a ser usada
        ///     diretamente do seu c�digo. Esta API pode ser alterada ou removida em vers�es futuras.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        ///     Esta API oferece suporte � infraestrutura de interface do usu�rio padr�o do ASP.NET Core Identity e n�o se destina a ser usada
        ///     diretamente do seu c�digo. Esta API pode ser alterada ou removida em vers�es futuras.
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
