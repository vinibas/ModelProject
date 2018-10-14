(function criaEmailContatoEvitandoSpammers() {
    const arroba = "@";
    const provedor = "gmail";
    const domínio = ".com";
    const usuário = "viny.bas";

    const endereçoCompleto = usuário + arroba + provedor + domínio;

    const email = document.getElementById("endereço-email");
    email.href = "mailto:" + endereçoCompleto;
    email.innerText = endereçoCompleto;
})();