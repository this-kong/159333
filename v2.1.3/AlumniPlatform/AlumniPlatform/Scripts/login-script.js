document.addEventListener('DOMContentLoaded', function () {
    // Role selection
    const roleCards = document.querySelectorAll('.role-card');
    roleCards.forEach(card => {
        card.addEventListener('click', function () {
            const role = this.dataset.role;

            // Update active card
            roleCards.forEach(c => c.classList.remove('active'));
            this.classList.add('active');

            // Show corresponding form
            document.querySelectorAll('.form-section').forEach(section => {
                section.classList.remove('active');
            });

            document.querySelector(`.form-section.${role}`).classList.add('active');
        });
    });

    // Switch between login/register
    const switchLinks = document.querySelectorAll('.switch-form');
    switchLinks.forEach(link => {
        link.addEventListener('click', function () {
            const role = this.dataset.role;
            const formType = this.dataset.form;

            // Hide all forms
            document.querySelectorAll('.form-section').forEach(section => {
                section.classList.remove('active');
            });

            // Show target form
            if (formType === 'register') {
                document.querySelector(`.form-section.${role}.register`).classList.add('active');
            } else {
                document.querySelector(`.form-section.${role}`).classList.add('active');
            }
        });
    });

    // Form validation
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function (e) {
            const requiredFields = form.querySelectorAll('[required]');
            let isValid = true;

            requiredFields.forEach(field => {
                if (!field.value.trim()) {
                    isValid = false;
                    field.classList.add('is-invalid');

                    // Add error message
                    if (!field.nextElementSibling || !field.nextElementSibling.classList.contains('invalid-feedback')) {
                        const errorDiv = document.createElement('div');
                        errorDiv.classList.add('invalid-feedback');
                        errorDiv.textContent = 'This field is required';
                        field.parentNode.appendChild(errorDiv);
                    }
                } else {
                    field.classList.remove('is-invalid');

                    // Remove error message
                    if (field.nextElementSibling && field.nextElementSibling.classList.contains('invalid-feedback')) {
                        field.nextElementSibling.remove();
                    }
                }
            });

            // Check password match for registration
            if (form.id.includes('Register')) {
                const password = form.querySelector('input[name="Password"]');
                const confirmPassword = form.querySelector('input[name="ConfirmPassword"]');

                if (password && confirmPassword && password.value !== confirmPassword.value) {
                    isValid = false;
                    confirmPassword.classList.add('is-invalid');

                    if (!confirmPassword.nextElementSibling || !confirmPassword.nextElementSibling.classList.contains('invalid-feedback')) {
                        const errorDiv = document.createElement('div');
                        errorDiv.classList.add('invalid-feedback');
                        errorDiv.textContent = 'Passwords do not match';
                        confirmPassword.parentNode.appendChild(errorDiv);
                    }
                }
            }

            if (!isValid) {
                e.preventDefault();
            }
        });
    });

    // Real-time validation for password match
    const passwordFields = document.querySelectorAll('input[name="Password"], input[name="ConfirmPassword"]');
    passwordFields.forEach(field => {
        field.addEventListener('input', function () {
            const password = document.querySelector('input[name="Password"]');
            const confirmPassword = document.querySelector('input[name="ConfirmPassword"]');

            if (password && confirmPassword && password.value !== confirmPassword.value) {
                confirmPassword.classList.add('is-invalid');

                if (!confirmPassword.nextElementSibling || !confirmPassword.nextElementSibling.classList.contains('invalid-feedback')) {
                    const errorDiv = document.createElement('div');
                    errorDiv.classList.add('invalid-feedback');
                    errorDiv.textContent = 'Passwords do not match';
                    confirmPassword.parentNode.appendChild(errorDiv);
                }
            } else {
                confirmPassword.classList.remove('is-invalid');

                if (confirmPassword.nextElementSibling && confirmPassword.nextElementSibling.classList.contains('invalid-feedback')) {
                    confirmPassword.nextElementSibling.remove();
                }
            }
        });
    });
});