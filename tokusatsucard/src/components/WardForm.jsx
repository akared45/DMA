import { useState, useEffect } from 'react';
import { Form, Button, Alert, Card, Spinner } from 'react-bootstrap';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function WardForm() {
  const { id } = useParams(); // id từ URL (nếu có → edit)
  const navigate = useNavigate();
  const isEdit = Boolean(id); // true = edit, false = create

  const [form, setForm] = useState({
    name: '',
    capacity: ''
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [validated, setValidated] = useState(false);

  // Nếu là edit → load dữ liệu cũ
  useEffect(() => {
    if (isEdit) {
      setLoading(true);
      api.get(`/wards/${id}`)
        .then(res => {
          setForm({
            name: res.data.name,
            capacity: res.data.capacity
          });
        })
        .catch(() => setError('Không tải được thông tin khoa'))
        .finally(() => setLoading(false));
    }
  }, [id, isEdit]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formEl = e.currentTarget;

    if (formEl.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
      return;
    }

    setLoading(true);
    setError('');

    const payload = {
      name: form.name.trim(),
      capacity: parseInt(form.capacity)
    };

    try {
      if (isEdit) {
        await api.put(`/wards/${id}`, { ...payload, wardId: parseInt(id) });
      } else {
        await api.post('/wards', payload);
      }
      navigate('/wards'); // quay về danh sách
    } catch (err) {
      const msg = err.response?.data?.title || err.response?.data || 'Lỗi server';
      setError(msg);
    } finally {
      setLoading(false);
    }
  };

  if (loading && isEdit) {
    return <Spinner animation="border" className="d-block mx-auto mt-5" />;
  }

  return (
    <Card className="mx-auto mt-5" style={{ maxWidth: '500px' }}>
      <Card.Header className="bg-primary text-white">
        <h4 className="mb-0">
          {isEdit ? 'Sửa Khoa' : 'Thêm Khoa Mới'}
        </h4>
      </Card.Header>

      <Card.Body>
        {error && <Alert variant="danger">{error}</Alert>}

        <Form noValidate validated={validated} onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Label>Tên khoa</Form.Label>
            <Form.Control
              required
              type="text"
              placeholder="VD: Khoa Nội, Khoa Nhi..."
              value={form.name}
              onChange={(e) => setForm({ ...form, name: e.target.value })}
              maxLength={100}
            />
            <Form.Control.Feedback type="invalid">
              Vui lòng nhập tên khoa (tối đa 100 ký tự)
            </Form.Control.Feedback>
          </Form.Group>

          <Form.Group className="mb-4">
            <Form.Label>Sức chứa (số giường)</Form.Label>
            <Form.Control
              required
              type="number"
              min="1"
              max="999"
              placeholder="VD: 50"
              value={form.capacity}
              onChange={(e) => setForm({ ...form, capacity: e.target.value })}
            />
            <Form.Control.Feedback type="invalid">
              Sức chứa phải là số từ 1 đến 999
            </Form.Control.Feedback>
          </Form.Group>

          <div className="d-flex gap-2">
            <Button 
              variant="primary" 
              type="submit" 
              disabled={loading}
            >
              {loading ? 'Đang lưu...' : (isEdit ? 'Cập nhật' : 'Tạo mới')}
            </Button>

            <Button 
              variant="secondary" 
              onClick={() => navigate('/wards')}
              disabled={loading}
            >
              Hủy
            </Button>
          </div>
        </Form>
      </Card.Body>
    </Card>
  );
}