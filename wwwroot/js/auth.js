window.submitLoginForm = (email, password, rememberMe) => {
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = '/login-handler?handler=Login';

    const emailField = document.createElement('input');
    emailField.type = 'hidden';
    emailField.name = 'email';
    emailField.value = email;
    form.appendChild(emailField);

    const passwordField = document.createElement('input');
    passwordField.type = 'hidden';
    passwordField.name = 'password';
    passwordField.value = password;
    form.appendChild(passwordField);

    const rememberMeField = document.createElement('input');
    rememberMeField.type = 'hidden';
    rememberMeField.name = 'rememberMe';
    rememberMeField.value = rememberMe;
    form.appendChild(rememberMeField);

    document.body.appendChild(form);
    form.submit();
};

window.submitRegisterForm = (email, password) => {
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = '/login-handler?handler=Register';

    const emailField = document.createElement('input');
    emailField.type = 'hidden';
    emailField.name = 'email';
    emailField.value = email;
    form.appendChild(emailField);

    const passwordField = document.createElement('input');
    passwordField.type = 'hidden';
    passwordField.name = 'password';
    passwordField.value = password;
    form.appendChild(passwordField);

    document.body.appendChild(form);
    form.submit();
};
