function validarFormatoPassword(novoPassword, confirmacaoNovoPassword, alteracaoSenha, passwordAtual) {

    if (novoPassword == "") {
        alert('A nova senha é obrigatória!');
        return false;
    }

    if (confirmacaoNovoPassword == "") {
        alert('A confirmação da nova senha é obrigatória!');
        return false;
    }

    if (alteracaoSenha && passwordAtual == "") {
        alert('A nova senha atual é obrigatória!');
        return false;
    }

    if (novoPassword.length < 6 || novoPassword.length > 15) {
        alert('A senha deve possuir entre 6 e 15 caracteres.');
        return false;
    }

    if (novoPassword != confirmacaoNovoPassword) {
        alert('A confirmação deve ser idêntica à nova senha informada');
        return false;
    }

    if (novoPassword != confirmacaoNovoPassword) {
        alert('A confirmação deve ser idêntica à nova senha informada');
        return false;
    }

    if (alteracaoSenha && passwordAtual == novoPassword) {
        alert('A nova senha deve ser diferente da Atual');
        return false;
    }



    return true;


}

function setUserLogedData(Nome,Login,Atribuicao) {
    window.localStorage.nomeUsuarioLogado = Nome;
    window.localStorage.loginUsuarioLogado = Login;
    window.localStorage.atribuicaoUsuarioLogado = Atribuicao;
}

function numericValidation(e) {
    // Allow: backspace, delete, tab, escape, enter and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}

function FormatarQuantidade(valor) {

    if (valor == 0)
        return valor;

    if (valor < 1000)
        return valor.toString() + " g";

    var val = (valor / 1000).toFixed(3);

    var parts = val.toString().split(".");

    valg = parseInt(parts[1]);
    valkg = parseInt(parts[0]);

    var result = valkg.toString() + " Kg";

    if (valg > 0)
        result += " e " + valg.toString() + " g";


    return result;



}