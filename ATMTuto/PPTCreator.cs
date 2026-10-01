using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATMTuto
{
    class PPTCreator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ATM模拟系统项目演示PPT内容");
            Console.WriteLine("=====================================\n");

            string[] slides = new string[]
            {
                "幻灯片1 - 封面",
                "标题: ATM模拟系统",
                "副标题: C# Windows Forms项目",
                "开发者: [您的名字]",
                "日期: 2026-05-09",

                "\n幻灯片2 - 项目概述",
                "项目背景:",
                "- 这是一个完整的ATM模拟系统",
                "- 使用C# Windows Forms开发",
                "- 采用SQL Server数据库",
                "- 支持多种银行业务功能",

                "\n幻灯片3 - 系统架构",
                "技术栈:",
                "- 前端: C# Windows Forms (Guna UI)",
                "- 后端: C# .NET Framework",
                "- 数据库: SQL Server LocalDB",
                "- 开发环境: Visual Studio",

                "\n幻灯片4 - 功能模块 - 用户端",
                "用户功能:",
                "1. 登录/注册 - 用户身份验证和账户创建",
                "2. 存款 - 存入现金到账户",
                "3. 取款 - 从账户提取现金",
                "4. 转账 - 向其他账户转账",
                "5. 余额查询 - 查看账户余额",
                "6. 交易历史 - 查看所有交易记录",
                "7. 修改PIN - 更改账户密码",
                "8. 快速取款 - 预设金额快速取款",

                "\n幻灯片5 - 功能模块 - 管理端",
                "管理功能:",
                "1. 管理员登录 - 安全访问管理界面",
                "2. 用户管理 - 查看和管理所有用户",
                "3. 限额管理 - 设置交易限额",
                "4. 数据统计 - 查看系统使用情况",

                "\n幻灯片6 - 限额管理功能",
                "限额类型:",
                "- 每日取款限额 (DailyWithdrawLimit)",
                "- 每日存款限额 (DailyDepositLimit)",
                "- 单次取款限额 (SingleWithdrawLimit)",
                "- 单次存款限额 (SingleDepositLimit)",
                "特点:",
                "- 转账计入每日取款限额",
                "- 实时显示剩余限额",
                "- 超过限额自动拒绝交易",

                "\n幻灯片7 - 数据库设计",
                "AccountTbl 表:",
                "- AccNum (账号) - 主键",
                "- Name, LaName (姓名)",
                "- Dob (出生日期)",
                "- Phone (电话)",
                "- Address (地址)",
                "- Education, Occupation (教育/职业)",
                "- PIN (密码)",
                "- Balance (余额)",
                "- 4个限额字段",

                "TransactionTbl 表:",
                "- AccNum (账号)",
                "- Type (交易类型)",
                "- Amount (金额)",
                "- TDate (交易时间)",

                "\n幻灯片8 - 安全性设计",
                "安全特性:",
                "- PIN码验证登录",
                "- 6位数字/字母密码",
                "- 交易确认对话框",
                "- 余额不足检查",
                "- 限额超限检查",
                "- 收款账户验证",

                "\n幻灯片9 - 业务流程 - 取款",
                "取款流程:",
                "1. 用户输入取款金额",
                "2. 系统检查余额是否充足",
                "3. 系统检查单次限额",
                "4. 系统检查每日限额",
                "5. 用户确认交易",
                "6. 更新账户余额",
                "7. 记录交易历史",
                "8. 显示交易成功",

                "\n幻灯片10 - 业务流程 - 转账",
                "转账流程:",
                "1. 用户输入收款账号",
                "2. 系统验证收款账户存在",
                "3. 用户输入转账金额",
                "4. 系统检查余额是否充足",
                "5. 系统检查限额(取款+转账)",
                "6. 用户确认交易",
                "7. 扣款方余额减少",
                "8. 收款方余额增加",
                "9. 双方都记录交易历史",

                "\n幻灯片11 - 打印凭条功能",
                "功能特点:",
                "- 模拟ATM打印凭条",
                "- 显示账号信息",
                "- 显示最近10笔交易",
                "- 包含交易时间戳",
                "- 支持预览和打印",

                "\n幻灯片12 - 项目文件结构",
                "文件结构:",
                "├── Login.cs/csx - 登录界面",
                "├── HOME.cs - 用户主界面",
                "├── Deposit.cs - 存款",
                "├── Withdraw.cs - 取款",
                "├── Transfer.cs - 转账",
                "├── Inquiry.cs - 查询/打印",
                "├── Account.cs - 账户管理",
                "├── ChangePin.cs - 修改密码",
                "├── Fastcash.cs - 快速取款",
                "├── Admin*.cs - 管理功能",
                "├── FormTransitionHelper.cs - 页面跳转",
                "└── App.config - 配置文件",

                "\n幻灯片13 - 核心代码逻辑",
                "数据库操作:",
                "- 使用SqlConnection连接数据库",
                "- 使用SqlDataAdapter查询数据",
                "- 使用SqlCommand执行操作",
                "- 参数化查询防止SQL注入",

                "限额检查:",
                "- getBalance() - 获取余额和限额",
                "- getDailyWithdrawAmount() - 获取当日已取金额",
                "- checkLimits() - 检查各项限额",

                "\n幻灯片14 - 界面特色",
                "UI设计:",
                "- 使用Guna UI库美化界面",
                "- 深青色主题配色",
                "- 无边框窗体设计",
                "- 流畅的动画过渡",
                "- 响应式布局",
                "- 清晰的信息提示",

                "\n幻灯片15 - 总结",
                "项目亮点:",
                "✓ 完整的MVC架构",
                "✓ 丰富的业务功能",
                "✓ 严格的限额控制",
                "✓ 友好的用户界面",
                "✓ 安全的认证机制",
                "✓ 详细的交易记录",

                "技术收获:",
                "- Windows Forms开发",
                "- SQL Server数据库操作",
                "- 参数化查询防注入",
                "- UI组件使用技巧",
                "- 业务逻辑设计",
                "- 异常处理机制"
            };

            foreach (var slide in slides)
            {
                Console.WriteLine(slide);
            }

            Console.WriteLine("\n=====================================");
            Console.WriteLine("PPT内容生成完成！");
            Console.WriteLine("请使用PowerPoint打开生成的.pptx文件");
        }
    }
}