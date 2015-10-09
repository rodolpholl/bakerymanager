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