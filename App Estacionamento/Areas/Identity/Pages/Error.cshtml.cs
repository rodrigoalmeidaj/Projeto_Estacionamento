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
    ///     Esta API oferece suporte à infraestrutura de interface do usuário padrão do ASP.NET Core Identity e não se destina a ser usada
    ///     diretamente do seu código. Esta API pode ser alterada ou removida em versões futuras.
    /// </summary>
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        /// <resumo>
        ///     Esta API oferece suporte à infraestrutura de interface do usuário padrão do ASP.NET Core Identity e não se destina a ser usada
        ///     diretamente do seu código. Esta API pode ser alterada ou removida em versões futuras.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        ///     Esta API oferece suporte à infraestrutura de interface do usuário padrão do ASP.NET Core Identity e não se destina a ser usada
        ///     diretamente do seu código. Esta API pode ser alterada ou removida em versões futuras.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        ///     Esta API oferece suporte à infraestrutura de interface do usuário padrão do ASP.NET Core Identity e não se destina a ser usada
        ///     diretamente do seu código. Esta API pode ser alterada ou removida em versões futuras.
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
