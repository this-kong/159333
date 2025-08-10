document.addEventListener("DOMContentLoaded", function () {
    // 元素获取
    const contacts = document.querySelectorAll("#contacts li");
    const chatWith = document.getElementById("chat-with");
    const chatMessages = document.getElementById("chat-messages");
    const messageInput = document.getElementById("message-input");
    const sendBtn = document.getElementById("send-btn");
    const addBtn = document.getElementById("add-contact-btn");
    const modal = document.getElementById("add-contact-modal");
    const closeBtn = document.querySelector(".close-btn");
    const toggleContactsBtn = document.getElementById('toggle-contacts');
    const contactList = document.querySelector('.contact-list');
    const chatArea = document.querySelector('.chat-area');
    const searchInput = document.getElementById("contact-search-input");
    const addSingleContactBtn = document.getElementById("add-single-contact");
    const createGroupBtn = document.getElementById("create-group");

    /* ---------------- 切换联系人 ---------------- */
    contacts.forEach(contact => {
        contact.addEventListener("click", () => {
            document.querySelector("#contacts li.active")?.classList.remove("active");
            contact.classList.add("active");
            chatWith.textContent = contact.dataset.name;
            chatMessages.innerHTML = "";

            // 窄屏点击后关闭联系人栏
            if (window.innerWidth <= 768) {
                contactList.classList.remove('show');
            }
        });
    });

    /* ---------------- 发送消息 ---------------- */
    function sendMessage() {
        const text = messageInput.value.trim();
        if (!text) return;

        // 发送方消息
        const msgDiv = document.createElement("div");
        msgDiv.classList.add("message", "sent");
        msgDiv.textContent = text;
        chatMessages.appendChild(msgDiv);
        messageInput.value = "";
        chatMessages.scrollTop = chatMessages.scrollHeight;

        // 模拟回复
        setTimeout(() => {
            const reply = document.createElement("div");
            reply.classList.add("message", "received");
            reply.textContent = "收到: " + text;
            chatMessages.appendChild(reply);
            chatMessages.scrollTop = chatMessages.scrollHeight;
        }, 1000);
    }

    sendBtn.addEventListener("click", sendMessage);
    messageInput.addEventListener("keypress", e => {
        if (e.key === "Enter") {
            e.preventDefault();
            sendMessage();
        }
    });

    /* ---------------- 弹窗控制 ---------------- */
    function openModal() {
        if (window.innerWidth <= 768) {
            contactList.classList.remove('show'); // 窄屏先收起侧边栏
        }
        modal.classList.add('open');
        modal.classList.remove('hidden'); // 确保是隐藏状态才移除
        //document.body.style.overflow = 'hidden'; // 禁止滚动
    }

    function closeModal() {
        modal.classList.remove('open');
        modal.classList.add('hidden');
        document.body.style.overflow = '';
    }

    addBtn.addEventListener('click', e => {
        e.stopPropagation();
        openModal();
    });

    closeBtn.addEventListener('click', e => {
        e.stopPropagation();
        closeModal();
    });

    modal.addEventListener('click', e => {
        if (e.target === modal) closeModal();
    });

    document.addEventListener('keydown', e => {
        if (e.key === 'Escape' && modal.classList.contains('open')) {
            closeModal();
        }
    });

    /* ---------------- 搜索联系人 ---------------- */
    searchInput.addEventListener("input", function () {
        const query = this.value.toLowerCase();
        document.querySelectorAll("#contacts li").forEach(item => {
            const name = item.textContent.toLowerCase();
            item.style.display = name.includes(query) ? "" : "none";
        });
    });

    /* ---------------- 跳转按钮 ---------------- */
    addSingleContactBtn.addEventListener("click", () => {
        window.location.href = "/Alumni/Search";
    });

    createGroupBtn.addEventListener("click", () => {
        window.location.href = "/Group/Create";
    });

    /* ---------------- 联系人栏切换 ---------------- */
    toggleContactsBtn.addEventListener('click', e => {
        e.stopPropagation();
        contactList.classList.toggle('show');
    });

    // 点击聊天区关闭联系人栏（仅窄屏）
    chatArea.addEventListener('click', () => {
        if (window.innerWidth <= 768 && contactList.classList.contains('show')) {
            contactList.classList.remove('show');
        }
    });

    /* ---------------- 输入框高度自适应 ---------------- */
    messageInput.addEventListener("input", function () {
        this.style.height = "auto";
        this.style.height = Math.min(this.scrollHeight, 120) + "px";
    });
});
