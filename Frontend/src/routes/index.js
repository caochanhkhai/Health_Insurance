//Layouts
import { HeaderOnly } from '~/components/Layout';

import Home from '~/pages/Home';
import XemGoiBaoHiem from '~/pages/XemGoiBaoHiem';
import NguoiDung_QuanTriVien from '~/pages/NguoiDung_QuanTriVien';

//public routes
const publicRoutes = [
    { path: '/', component: Home },
    //chú ý viết hoa viết thường
    { path: '/xemGoiBaoHiem', component: XemGoiBaoHiem },
];
const privateRoutes = [{ path: '/nguoiDung_QuanTriVien', component: NguoiDung_QuanTriVien, layout: HeaderOnly }];

export { publicRoutes, privateRoutes };
