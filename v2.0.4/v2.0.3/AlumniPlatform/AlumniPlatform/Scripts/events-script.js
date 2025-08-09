document.addEventListener('DOMContentLoaded', function () {
    // 渲染活动卡片
    function renderEvents(events, filter = 'all') {
        const container = document.getElementById('events-container');
        if (!container) {
            console.error('找不到活动容器');
            return;
        }
        // 筛选逻辑
        const filteredEvents = filter === 'all'
            ? events
            : events.filter(event => event.category === filter);
        // 清空容器
        container.innerHTML = '';

        // 如果没有数据提示
        if (filteredEvents.length === 0) {
            container.innerHTML = `
                <div class="col-12 text-center py-5">
                    <i class="fas fa-calendar-times fa-2x text-muted"></i>
                    <p class="mt-3">暂无活动数据</p>
                </div>
            `;
            return;
        }

        // 渲染每个活动卡片
        filteredEvents.forEach(event => {
            const eventCard = document.createElement('div');
            eventCard.className = 'col-lg-4 col-md-6';
            eventCard.innerHTML = `
                <div class="event-card">
                    <img src="${event.image || '/images/default-event.jpg'}" 
                         class="card-img-top event-img" 
                         alt="${event.title}">
                    <div class="card-body">
                        <div class="event-category ${event.category}-category">
                            <i class="${getCategoryIcon(event.category)}"></i> 
                            ${getCategoryName(event.category)}
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
                        <a href="/Events/Details/${event.id}" class="btn-view-details">
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
        const iconMap = {
            'meetup': 'fas fa-users',
            'social': 'fas fa-glass-cheers',
            'networking': 'fas fa-glass-cheers',
            'workshop': 'fas fa-chalkboard-teacher'
        };
        return iconMap[category] || 'fas fa-calendar';
    }

    // 获取分类名称
    function getCategoryName(category) {
        const nameMap = {
            'meetup': '校友聚会',
            'social': '社交活动',
            'networking': '社交活动',
            'workshop': '工作坊'
        };
        return nameMap[category] || '活动';
    }

    // 初始化事件监听
    function initEventListeners() {
        // 分类筛选按钮
        const eventTypeButtons = document.querySelectorAll('.event-type-btn');
        eventTypeButtons.forEach(button => {
            button.addEventListener('click', function () {
                // 更新按钮状态
                eventTypeButtons.forEach(btn => btn.classList.remove('active'));
                this.classList.add('active');
                // 获取筛选类型并重新渲染
                const eventType = this.dataset.type;
                if (window.eventData) {
                    renderEvents(window.eventData, eventType);
                }
            });
        });

        //// 发布活动按钮
        //const createEventBtn = document.querySelector('.btn-create-event');
        //if (createEventBtn) {
        //    createEventBtn.addEventListener('click', function (e) {
        //        e.preventDefault();
        //        alert('发布新活动功能将在登录后可用');
        //    });
        //}
    }

    // 页面初始化
    function init() {
        // 检查是否有数据注入
        if (!window.eventData || window.eventData.length === 0) {
            console.warn('没有活动数据被注入');
            document.getElementById('events-container').innerHTML = `
                <div class="col-12 text-center py-5">
                    <i class="fas fa-exclamation-triangle fa-2x text-warning"></i>
                    <p class="mt-3">未能加载活动数据</p>
                </div>
            `;
            return;
        }

        // 初始渲染
        renderEvents(window.eventData);

        // 初始化事件监听
        initEventListeners();
    }

    // 启动初始化
    init();
});