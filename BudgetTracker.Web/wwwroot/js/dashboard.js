// Dashboard Chart Functionality
window.initializeIncomeExpensesChart = (canvasId, chartData) => {
    const canvas = document.getElementById(canvasId);
    if (!canvas) {
        return;
    }
    
    const ctx = canvas.getContext('2d');
    const { width, height } = canvas;
    
    // Clear canvas
    ctx.clearRect(0, 0, width, height);
    
    // Chart configuration - increased left padding for Y-axis labels
    const padding = { left: 60, right: 30, top: 30, bottom: 40 };
    const chartWidth = width - padding.left - padding.right;
    const chartHeight = height - padding.top - padding.bottom;
    const barGroupWidth = chartWidth / chartData.length;
    const barWidth = (barGroupWidth * 0.35); // Wider bars for better visibility
    const barSpacing = barGroupWidth * 0.1; // Space between income/expense bars
    const maxValue = Math.max(...chartData.map(d => Math.max(d.income, d.expenses)));
    
    // Colors - enhanced for better contrast
    const incomeColor = '#10b981'; // green
    const expenseColor = '#ef4444'; // red for better contrast
    const textColor = '#a0a3bd';
    const gridColor = '#404040'; // Lighter grid color for better visibility
    const labelColor = '#ffffff'; // white for value labels
    
    // Draw horizontal grid lines with better visibility
    ctx.strokeStyle = gridColor;
    ctx.lineWidth = 1.5;
    ctx.setLineDash([2, 2]);
    
    for (let i = 0; i <= 4; i++) {
        const y = padding.top + (chartHeight / 4) * i;
        ctx.beginPath();
        ctx.moveTo(padding.left, y);
        ctx.lineTo(width - padding.right, y);
        ctx.stroke();
        
        // Add grid value labels on the left with better positioning
        if (i < 4) {
            const gridValue = maxValue - (maxValue / 4) * i;
            ctx.fillStyle = textColor;
            ctx.font = '12px -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif';
            ctx.textAlign = 'right';
            ctx.textBaseline = 'middle';
            ctx.fillText(`$${(gridValue / 1000).toFixed(1)}k`, padding.left - 10, y);
        }
    }
    
    ctx.setLineDash([]);
    
    // Draw bars with value labels
    chartData.forEach((dataPoint, index) => {
        const groupCenterX = padding.left + (index * barGroupWidth) + (barGroupWidth / 2);
        const incomeBarX = groupCenterX - barWidth - (barSpacing / 2);
        const expenseBarX = groupCenterX + (barSpacing / 2);
        
        // Income bar (left)
        const incomeHeight = (dataPoint.income / maxValue) * chartHeight;
        const incomeY = padding.top + chartHeight - incomeHeight;
        
        ctx.fillStyle = incomeColor;
        ctx.fillRect(incomeBarX, incomeY, barWidth, incomeHeight);
        
        // Income value label
        if (incomeHeight > 20) {
            ctx.fillStyle = labelColor;
            ctx.font = 'bold 11px -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif';
            ctx.textAlign = 'center';
            ctx.textBaseline = 'bottom';
            const incomeValue = `$${(dataPoint.income / 1000).toFixed(1)}k`;
            ctx.fillText(incomeValue, incomeBarX + (barWidth / 2), incomeY - 6);
        }
        
        // Expense bar (right)
        const expenseHeight = (dataPoint.expenses / maxValue) * chartHeight;
        const expenseY = padding.top + chartHeight - expenseHeight;
        
        ctx.fillStyle = expenseColor;
        ctx.fillRect(expenseBarX, expenseY, barWidth, expenseHeight);
        
        // Expense value label
        if (expenseHeight > 20) {
            ctx.fillStyle = labelColor;
            ctx.font = 'bold 11px -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif';
            ctx.textAlign = 'center';
            ctx.textBaseline = 'bottom';
            const expenseValue = `$${(dataPoint.expenses / 1000).toFixed(1)}k`;
            ctx.fillText(expenseValue, expenseBarX + (barWidth / 2), expenseY - 6);
        }
        
        // Month label at bottom
        ctx.fillStyle = textColor;
        ctx.font = '12px -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif';
        ctx.textAlign = 'center';
        ctx.textBaseline = 'top';
        ctx.fillText(dataPoint.month, groupCenterX, height - padding.bottom + 8);
    });
    
    // Enhanced legend with better positioning
    const legendY = 15;
    const legendSpacing = 100;
    
    // Income legend
    ctx.fillStyle = incomeColor;
    ctx.fillRect(padding.left, legendY, 14, 14);
    ctx.fillStyle = textColor;
    ctx.font = '13px -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif';
    ctx.textAlign = 'left';
    ctx.textBaseline = 'middle';
    ctx.fillText('Income', padding.left + 22, legendY + 7);
    
    // Expense legend
    ctx.fillStyle = expenseColor;
    ctx.fillRect(padding.left + legendSpacing, legendY, 14, 14);
    ctx.fillStyle = textColor;
    ctx.fillText('Expenses', padding.left + legendSpacing + 22, legendY + 7);
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
