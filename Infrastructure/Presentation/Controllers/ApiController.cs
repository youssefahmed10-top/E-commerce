using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]

    public class ApiController :ControllerBase
    {
    }
}
