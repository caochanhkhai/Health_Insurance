import classNames from 'classnames/bind';
import styles from './Header.module.scss';

const cx = classNames.bind(styles);
function Header() {
    return (
        <header className={cx('wrapper')}>
            <div className="logo"></div>
            <div className="task"></div>
            <div className="login"></div>
        </header>
    );
}

export default Header;
