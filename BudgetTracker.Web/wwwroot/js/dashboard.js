// Dashboard Chart Functionality
window.initializeIncomeExpensesChart = (canvasId, chartData) => {
    const canvas = document.getElementById(canvasId);
    if (!canvas) {
        // Canvas not found - chart will not render
        return;
    }
    
    const ctx = canvas.getContext('2d');
    const { width, height } = canvas;
    
    // Clear canvas
    ctx.clearRect(0, 0, width, height);
    
    // Chart configuration
    const padding = 40;
    const chartWidth = width - (padding * 2);
    const chartHeight = height - (padding * 2);
    const barWidth = chartWidth / (chartData.length * 2);
    const maxValue = Math.max(...chartData.map(d => Math.max(d.income, d.expenses)));
    
    // Colors
    const incomeColor = '#10b981'; // green
    const expenseColor = '#3b82f6'; // blue
    const textColor = '#a0a3bd';
    const gridColor = '#2a2d3e';
    
    // Draw grid lines
    ctx.strokeStyle = gridColor;
    ctx.lineWidth = 1;
    for (let i = 0; i <= 4; i++) {
        const y = padding + (chartHeight / 4) * i;
        ctx.beginPath();
        ctx.moveTo(padding, y);
        ctx.lineTo(width - padding, y);
        ctx.stroke();
    }
    
    // Draw bars
    chartData.forEach((dataPoint, index) => {
        const x = padding + (index * barWidth * 2);
        
        // Income bar (left)
        const incomeHeight = (dataPoint.income / maxValue) * chartHeight;
        const incomeY = height - padding - incomeHeight;
        
        ctx.fillStyle = incomeColor;
        ctx.fillRect(x, incomeY, barWidth * 0.8, incomeHeight);
        
        // Expense bar (right)
        const expenseHeight = (dataPoint.expenses / maxValue) * chartHeight;
        const expenseY = height - padding - expenseHeight;
        
        ctx.fillStyle = expenseColor;
        ctx.fillRect(x + barWidth, expenseY, barWidth * 0.8, expenseHeight);
        
        // Month label
        ctx.fillStyle = textColor;
        ctx.font = '12px -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif';
        ctx.textAlign = 'center';
        const labelX = x + barWidth;
        ctx.fillText(dataPoint.month, labelX, height - 10);
    });
    
    // Add chart legend
    const legendY = 20;
    
    // Income legend
    ctx.fillStyle = incomeColor;
    ctx.fillRect(padding, legendY, 12, 12);
    ctx.fillStyle = textColor;
    ctx.font = '12px -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif';
    ctx.textAlign = 'left';
    ctx.fillText('Income', padding + 20, legendY + 9);
    
    // Expense legend
    ctx.fillStyle = expenseColor;
    ctx.fillRect(padding + 80, legendY, 12, 12);
    ctx.fillStyle = textColor;
    ctx.fillText('Expenses', padding + 100, legendY + 9);
};

// Utility functions for dashboard
window.dashboardUtils = {
    formatCurrency: (amount) => {
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD'
        }).format(amount);
    },
    
    formatPercentage: (value) => {
        return `${value >= 0 ? '+' : ''}${value.toFixed(1)}%`;
    },
    
    animateValue: (element, start, end, duration = 1000) => {
        const range = end - start;
        const increment = range / (duration / 16);
        let current = start;
        
        const timer = setInterval(() => {
            current += increment;
            if ((increment > 0 && current >= end) || (increment < 0 && current <= end)) {
                current = end;
                clearInterval(timer);
            }
            element.textContent = window.dashboardUtils.formatCurrency(current);
        }, 16);
    }
};

// Initialize dashboard animations when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    // Add smooth transitions to cards
    const cards = document.querySelectorAll('.balance-card, .income-expenses-section, .recent-transactions');
    cards.forEach((card, index) => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(20px)';
        
        setTimeout(() => {
            card.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }, index * 100);
    });
});

// Returns the center point of an element (for morph origin)
window.getElementCenter = (element) => {
    if (!element) return { X: window.innerWidth / 2, Y: window.innerHeight / 2 };
    const rect = element.getBoundingClientRect();
    return {
        X: rect.left + rect.width / 2,
        Y: rect.top + rect.height / 2
    };
};

// Convenience: open modal from an element by computing its center
window.openMorphModalFromElement = (element) => {
    try {
        const rect = element.getBoundingClientRect();
        const x = rect.left + rect.width / 2;
        const y = rect.top + rect.height / 2;
        window.openMorphModal(x, y);
    } catch {
        window.openMorphModal(0, 0);
    }
};

// Morph modal open/close controlled via JS only
window.openMorphModal = (originX, originY) => {
    const modal = document.getElementById('morphModal');
    if (!modal) return;
    modal.style.setProperty('--origin-x', `${originX}px`);
    modal.style.setProperty('--origin-y', `${originY}px`);
    modal.classList.add('is-visible');
    modal.classList.remove('is-closing');
    // Next tick to allow display to apply
    requestAnimationFrame(() => {
        modal.classList.add('is-open');
    });
};

window.closeMorphModal = () => {
    const modal = document.getElementById('morphModal');
    if (!modal) return;
    modal.classList.add('is-closing');
    modal.classList.remove('is-open');
    setTimeout(() => {
        modal.classList.remove('is-visible');
        modal.classList.remove('is-closing');
    }, 320); // match CSS duration
};
