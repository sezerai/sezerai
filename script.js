class AILoginSystem {
    constructor() {
        this.initializeEventListeners();
        this.startBehaviorAnalysis();
        this.generateCaptcha();
        this.logAIEvent('Sistem başlatıldı', 'info');
    }

    initializeEventListeners() {
        const form = document.getElementById('loginForm');
        const usernameInput = document.getElementById('username');
        const passwordInput = document.getElementById('password');
        const captchaInput = document.getElementById('captchaAnswer');

        usernameInput.addEventListener('input', () => this.analyzeUsername());
        passwordInput.addEventListener('input', () => this.analyzePassword());
        captchaInput.addEventListener('input', () => this.validateCaptcha());
        form.addEventListener('submit', (e) => this.handleLogin(e));
    }

    startBehaviorAnalysis() {
        let progress = 0;
        const progressBar = document.getElementById('progressBar');
        const analysisInterval = setInterval(() => {
            progress += Math.random() * 10;
            if (progress >= 100) {
                progress = 100;
                clearInterval(analysisInterval);
                this.enableLoginButton();
            }
            progressBar.style.width = `${progress}%`;
        }, 200);
    }

    analyzeUsername() {
        const username = document.getElementById('username').value;
        const feedback = document.getElementById('usernameFeedback');
        
        if (username.length === 0) {
            feedback.textContent = '';
            return;
        }

        // AI benzeri analiz kuralları
        if (username.length < 3) {
            feedback.textContent = '⚠️ Kullanıcı adı çok kısa';
            feedback.className = 'input-feedback feedback-warning';
        } else if (this.containsSuspiciousPatterns(username)) {
            feedback.textContent = '🚩 Şüpheli desen tespit edildi';
            feedback.className = 'input-feedback feedback-invalid';
            this.updateRiskLevel('high');
        } else if (this.isCommonUsername(username)) {
            feedback.textContent = '⚠️ Yaygın kullanılan bir kullanıcı adı';
            feedback.className = 'input-feedback feedback-warning';
            this.updateRiskLevel('medium');
        } else {
            feedback.textContent = '✅ Kullanıcı adı uygun';
            feedback.className = 'input-feedback feedback-valid';
            this.updateRiskLevel('low');
        }

        this.logAIEvent(`Kullanıcı adı analizi: ${username}`, 'analysis');
    }

    analyzePassword() {
        const password = document.getElementById('password').value;
        const feedback = document.getElementById('passwordFeedback');
        
        if (password.length === 0) {
            feedback.textContent = '';
            return;
        }

        let strength = 0;
        let messages = [];

        // AI benzeri şifre analizi
        if (password.length >= 8) strength++;
        else messages.push('en az 8 karakter');

        if (/[A-Z]/.test(password)) strength++;
        else messages.push('büyük harf');

        if (/[a-z]/.test(password)) strength++;
        else messages.push('küçük harf');

        if (/[0-9]/.test(password)) strength++;
        else messages.push('rakam');

        if (/[^A-Za-z0-9]/.test(password)) strength++;
        else messages.push('özel karakter');

        if (strength >= 4) {
            feedback.textContent = '✅ Güçlü şifre';
            feedback.className = 'input-feedback feedback-valid';
        } else if (strength >= 2) {
            feedback.textContent = '⚠️ Orta seviye şifre';
            feedback.className = 'input-feedback feedback-warning';
        } else {
            feedback.textContent = `❌ Zayıf şifre. Eksik: ${messages.join(', ')}`;
            feedback.className = 'input-feedback feedback-invalid';
        }

        this.logAIEvent('Şifre güçlülüğü analiz edildi', 'analysis');
    }

    generateCaptcha() {
        const questions = [
            "3 + 5 kaç eder? (rakamla yazın)",
            "İlk harfi A olan bir meyve yazın",
            "10 - 4 kaç eder? (rakamla yazın)",
            "Türkiye'nin başkenti neresidir?",
            "Güneş sistemindeki gezegen sayısı? (rakamla yazın)"
        ];
        
        const answers = ["8", "armut", "elma", "portakal", "6", "ankara", "8"];
        
        const randomIndex = Math.floor(Math.random() * questions.length);
        const question = questions[randomIndex];
        const answer = answers[randomIndex];
        
        document.getElementById('captchaQuestion').textContent = question;
        document.getElementById('captchaAnswer').dataset.expected = answer;
        
        this.logAIEvent('Akıllı CAPTCHA oluşturuldu', 'security');
    }

    validateCaptcha() {
        const answer = document.getElementById('captchaAnswer').value.toLowerCase();
        const expected = document.getElementById('captchaAnswer').dataset.expected.toLowerCase();
        const feedback = document.getElementById('captchaFeedback');
        
        if (answer === '') {
            feedback.textContent = '';
            return;
        }

        if (answer === expected) {
            feedback.textContent = '✅ CAPTCHA doğru';
            feedback.className = 'captcha-feedback feedback-valid';
        } else {
            feedback.textContent = '❌ CAPTCHA yanlış';
            feedback.className = 'captcha-feedback feedback-invalid';
        }
    }

    containsSuspiciousPatterns(text) {
        const suspiciousPatterns = [
            'admin', 'root', 'test', 'or 1=1', 'select', 'union', 'drop', 'insert'
        ];
        return suspiciousPatterns.some(pattern => 
            text.toLowerCase().includes(pattern.toLowerCase())
        );
    }

    isCommonUsername(username) {
        const commonUsernames = ['admin', 'user', 'test', 'demo', 'guest'];
        return commonUsernames.includes(username.toLowerCase());
    }

    updateRiskLevel(level) {
        const riskElement = document.getElementById('riskLevel');
        riskElement.textContent = 
            level === 'high' ? 'Yüksek Risk' : 
            level === 'medium' ? 'Orta Risk' : 'Düşük Risk';
        
        riskElement.className = `risk-level ${level}`;
        
        this.logAIEvent(`Risk seviyesi güncellendi: ${level}`, 'security');
    }

    enableLoginButton() {
        const button = document.getElementById('loginBtn');
        button.disabled = false;
        this.logAIEvent('Davranış analizi tamamlandı, giriş aktif', 'success');
    }

    async handleLogin(e) {
        e.preventDefault();
        
        const button = document.getElementById('loginBtn');
        const buttonText = document.querySelector('.btn-text');
        const loader = document.getElementById('btnLoader');
        
        // Buton durumunu güncelle
        buttonText.style.opacity = '0';
        loader.style.display = 'block';
        button.disabled = true;

        // AI güvenlik kontrolü
        const securityCheck = await this.performSecurityCheck();
        
        if (!securityCheck.passed) {
            this.showLoginResult('error', securityCheck.message);
            buttonText.style.opacity = '1';
            loader.style.display = 'none';
            button.disabled = false;
            return;
        }

        // Simüle edilmiş giriş işlemi
        setTimeout(() => {
            this.showLoginResult('success', 'Başarıyla giriş yapıldı! AI güvenlik kontrolünden geçtiniz.');
            buttonText.style.opacity = '1';
            loader.style.display = 'none';
        }, 2000);
    }

    async performSecurityCheck() {
        this.logAIEvent('Güvenlik kontrolü başlatıldı', 'security');
        
        // Simüle edilmiş AI güvenlik kontrolleri
        await this.delay(1000);
        
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;
        const captcha = document.getElementById('captchaAnswer').value;
        const expectedCaptcha = document.getElementById('captchaAnswer').dataset.expected;

        // Çeşitli güvenlik kontrolleri
        if (captcha.toLowerCase() !== expectedCaptcha.toLowerCase()) {
            this.logAIEvent('CAPTCHA doğrulama başarısız', 'security');
            return { passed: false, message: 'CAPTCHA doğrulaması başarısız' };
        }

        if (this.containsSuspiciousPatterns(username)) {
            this.logAIEvent('Şüpheli kullanıcı adı tespit edildi', 'security');
            return { passed: false, message: 'Güvenlik ihlali tespit edildi' };
        }

        if (password.length < 6) {
            this.logAIEvent('Zayıf şifre tespit edildi', 'security');
            return { passed: false, message: 'Şifre çok zayıf' };
        }

        this.logAIEvent('Tüm güvenlik kontrolleri başarılı', 'success');
        return { passed: true, message: 'Güvenlik kontrolü başarılı' };
    }

    showLoginResult(type, message) {
        this.logAIEvent(message, type);
        
        // Gerçek uygulamada burada kullanıcıyı yönlendirebilirsiniz
        alert(`${type === 'success' ? '✅' : '❌'} ${message}`);
    }

    logAIEvent(message, type = 'info') {
        const logContent = document.getElementById('logContent');
        const timestamp = new Date().toLocaleTimeString();
        
        const logEntry = document.createElement('div');
        logEntry.className = 'log-entry';
        logEntry.innerHTML = `
            <span class="log-time">[${timestamp}]</span>
            <span class="log-type-${type}">${message}</span>
        `;
        
        logContent.prepend(logEntry);
        
        // Log sayısını sınırla
        const entries = logContent.getElementsByClassName('log-entry');
        if (entries.length > 10) {
            entries[entries.length - 1].remove();
        }
    }

    delay(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
}

// Sayfa yüklendiğinde sistemi başlat
document.addEventListener('DOMContentLoaded', () => {
    new AILoginSystem();
});