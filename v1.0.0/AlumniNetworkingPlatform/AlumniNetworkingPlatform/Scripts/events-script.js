document.addEventListener('DOMContentLoaded', function () {
    // 活动数据
    const events = [
        {
            id: 1,
            title: "2023年度校友联谊晚宴",
            category: "meetup",
            date: "2023年12月15日 | 18:30",
            location: "城市大酒店宴会厅",
            attendees: 85,
            description: "一年一度的校友联谊晚宴，邀请所有校友参加，共同回顾美好时光，展望未来合作机会。",
            image: "https://images.unsplash.com/photo-1527525443983-6e60c75fff46?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80"
        },
        {
            id: 2,
            title: "创业校友交流酒会",
            category: "social",
            date: "2023年11月25日 | 19:00",
            location: "创新中心顶层酒吧",
            attendees: 42,
            description: "专为创业校友举办的交流酒会，提供轻松的环境让创业者们互相交流经验、分享资源。",
            image: "https://images.unsplash.com/photo-1540575467063-178a50c2df87?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80"
        },
        {
            id: 3,
            title: "人工智能行业趋势分析",
            category: "workshop",
            date: "2023年12月5日 | 14:00-17:00",
            location: "科技园会议中心3楼",
            attendees: 63,
            description: "由AI领域资深校友主讲，深入分析当前人工智能行业发展趋势与就业机会。",
            image: ""
        },

    ];

    // 渲染活动卡片
    function renderEvents(filter = 'all') {
        const container = document.getElementById('events-container');
        container.innerHTML = '';

        const filteredEvents = filter === 'all' ? events : events.filter(event => event.category === filter);

        filteredEvents.forEach(event => {
            const eventCard = document.createElement('div');
            eventCard.className = 'col-lg-4 col-md-6';
            eventCard.innerHTML = `
                <div class="event-card">
                    <img src="${event.image}" class="card-img-top event-img" alt="${event.title}">
                    <div class="card-body">
                        <div class="event-category ${event.category}-category">
                            <i class="${getCategoryIcon(event.category)}"></i> ${getCategoryName(event.category)}
                        </div>
                        <h3 class="event-title">${event.title}</h3>
                        <div class="event-details">
                            <i class="fas fa-calendar-day"></i>
                            <span>${event.date}</span>
                        </div>
                        <div class="event-details">
                            <i class="fas fa-map-marker-alt"></i>
                            <span>${event.location}</span>
                        </div>
                        <div class="event-details">
                            <i class="fas fa-user-friends"></i>
                            <span>已报名：${event.attendees}人</span>
                        </div>
                        <p class="event-description">${event.description}</p>
                        <a href="#" class="btn-view-details">
                            查看详情 <i class="fas fa-arrow-right"></i>
                        </a>
                    </div>
                </div>
            `;
            container.appendChild(eventCard);
        });
    }

    // 获取分类图标
    function getCategoryIcon(category) {
        switch (category) {
            case 'meetup': return 'fas fa-users';
            case 'networking': return 'fas fa-glass-cheers';
            case 'workshop': return 'fas fa-chalkboard-teacher';
            default: return 'fas fa-calendar';
        }
    }

    // 获取分类名称
    function getCategoryName(category) {
        switch (category) {
            case 'meetup': return '校友聚会';
            case 'networking': return '社交活动';
            case 'workshop': return '工作坊';
            default: return '活动';
        }
    }

    // 活动类型选择器事件
    const eventTypeButtons = document.querySelectorAll('.event-type-btn');
    eventTypeButtons.forEach(button => {
        button.addEventListener('click', function () {
            eventTypeButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');
            const eventType = this.dataset.type;
            renderEvents(eventType);
        });
    });

    // 发布活动按钮事件
    const createEventBtn = document.querySelector('.btn-create-event');
    createEventBtn.addEventListener('click', function () {
        alert('发布新活动功能将在登录后可用');
    });

    // 初始渲染所有活动
    renderEvents();
});