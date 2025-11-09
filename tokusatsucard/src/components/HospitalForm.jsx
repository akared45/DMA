import { useState, useEffect } from 'react';
import { Form, Button, Alert, Card } from 'react-bootstrap';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function HospitalForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const isEdit = Boolean(id);

  const [form, setForm] = useState({ name: '', location: '' });
  const [error, setError] = useState('');
  const [validated, setValidated] = useState(false);

  useEffect(() => {
    if (isEdit) {
      api.get(`/hospitals/${id}`).then(res => setForm(res.data))
        .catch(() => setError('Không tải được thông tin bệnh viện'));
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

    try {
      if (isEdit) {
        await api.put(`/hospitals/${id}`, { ...form, hospitalId: id });
      } else {
        await api.post('/hospitals', form);
      }
      navigate('/hospitals');
    } catch (err) {
      setError(err.response?.data || 'Lỗi server');
    }
  };

  return (
    <Card className="mx-auto mt-4" style={{ maxWidth: '600px' }}>
      <Card.Header>
        <h4>{isEdit ? 'Sửa' : 'Thêm'} Bệnh viện</h4>
      </Card.Header>
      <Card.Body>
        {error && <Alert variant="danger">{error}</Alert>}
        <Form noValidate validated={validated} onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Label>Tên bệnh viện</Form.Label>
            <Form.Control
              required
              type="text"
              value={form.name || ''}
              onChange={e => setForm({ ...form, name: e.target.value })}
            />
            <Form.Control.Feedback type="invalid">
              Vui lòng nhập tên bệnh viện
            </Form.Control.Feedback>
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Địa chỉ / Vị trí</Form.Label>
            <Form.Control
              required
              type="text"
              value={form.location || ''}
              onChange={e => setForm({ ...form, location: e.target.value })}
            />
            <Form.Control.Feedback type="invalid">
              Vui lòng nhập địa chỉ
            </Form.Control.Feedback>
          </Form.Group>

          <div className="d-flex gap-2">
            <Button variant="primary" type="submit">
              {isEdit ? 'Cập nhật' : 'Tạo mới'}
            </Button>
            <Button variant="secondary" onClick={() => navigate('/hospitals')}>
              Hủy
            </Button>
          </div>
        </Form>
      </Card.Body>
    </Card>
  );
}
